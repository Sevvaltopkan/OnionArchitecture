using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.OrderDetailQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderDetailResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetOrderDetailByIdQueryHandler : IQueryHandler<GetOrderDetailByIdQuery, OrderDetailQueryResult>
    {
        private readonly IOrderDetailRepository _repository;

        public GetOrderDetailByIdQueryHandler(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDetailQueryResult>> Handle(
            GetOrderDetailByIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                OrderDetail value = await _repository.GetByIdAsync(query.Id);

                if (value == null)
                    return Result<OrderDetailQueryResult>.Failure($"ID: {query.Id} bulunamadı");

                var result = new OrderDetailQueryResult
                {
                    Id = value.Id,
                    OrderId = value.OrderId,
                    ProductId = value.ProductId,
                    ProductName = value.Product?.ProductName,
                    UnitPrice = value.Product?.UnitPrice
                };

                return Result<OrderDetailQueryResult>.Success(result, "Sipariş detayı başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<OrderDetailQueryResult>.Failure("Sipariş detayı getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

