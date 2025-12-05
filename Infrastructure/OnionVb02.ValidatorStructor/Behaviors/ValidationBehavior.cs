using FluentValidation;
using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;

namespace OnionVb02.ValidatorStructor.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var errorMessages = failures.Select(f => f.ErrorMessage).ToList();

                if (typeof(TResponse).IsGenericType &&
                    typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var resultType = typeof(TResponse);
                    var validationFailureMethod = resultType.GetMethod("ValidationFailure");
                    if (validationFailureMethod != null)
                    {
                        return (TResponse)validationFailureMethod.Invoke(null, new object[] { errorMessages })!;
                    }
                }

                if (typeof(TResponse) == typeof(Result))
                {
                    return (TResponse)(object)Result.ValidationFailure(errorMessages);
                }

                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
