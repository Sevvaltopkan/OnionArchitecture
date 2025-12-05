using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.OrderDetailQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderDetailResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetOrderDetailsByOrderIdQueryHandler : IQueryHandler<GetOrderDetailsByOrderIdQuery, List<OrderDetailQueryResult>>
    {
        private readonly IOrderDetailRepository _repository;

        public GetOrderDetailsByOrderIdQueryHandler(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<OrderDetailQueryResult>>> Handle(
            GetOrderDetailsByOrderIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                var values = _repository.Where(x => x.OrderId == query.OrderId).ToList();

                var result = values.Select(x => new OrderDetailQueryResult
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    ProductName = x.Product?.ProductName,
                    UnitPrice = x.Product?.UnitPrice
                }).ToList();

                return Result<List<OrderDetailQueryResult>>.Success(result, "Sipariş detayları başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<List<OrderDetailQueryResult>>.Failure("Sipariş detayları getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

