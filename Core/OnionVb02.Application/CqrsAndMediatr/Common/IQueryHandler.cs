using MediatR;

namespace OnionVb02.Application.CqrsAndMediatr.Common
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}
