using MediatR;

namespace OnionVb02.Application.CqrsAndMediatr.Common
{
    public interface IQuery<TResult> : IRequest<Result<TResult>>
    {
    }
}
