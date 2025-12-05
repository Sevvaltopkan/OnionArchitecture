using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnionVb02.ValidatorStructor.Behaviors;
using OnionVb02.ValidatorStructor.Validators.CategoryValidators;

namespace OnionVb02.ValidatorStructor.DependencyResolvers
{
    public static class ValidatorResolver
    {
        public static void AddValidatorService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
