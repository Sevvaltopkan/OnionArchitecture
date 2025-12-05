using MediatR;

namespace OnionVb02.Application.CqrsAndMediatr.Common
{
    public interface ICommand<TResult> : IRequest<Result<TResult>>
    {
    }

    public interface ICommand : IRequest<Result>
    {
    }
}
