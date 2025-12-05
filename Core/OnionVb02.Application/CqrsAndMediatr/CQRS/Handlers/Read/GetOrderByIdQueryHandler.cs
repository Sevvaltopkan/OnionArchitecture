using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.OrderQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderQueryResult>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderQueryResult>> Handle(
            GetOrderByIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                Order value = await _repository.GetByIdAsync(query.Id);

                if (value == null)
                    return Result<OrderQueryResult>.Failure($"ID: {query.Id} bulunamadı");

                var result = new OrderQueryResult
                {
                    Id = value.Id,
                    ShippingAddress = value.ShippingAddress,
                    AppUserId = value.AppUserId,
                    UserName = value.AppUser?.UserName,
                    CreatedDate = value.CreatedDate
                };

                return Result<OrderQueryResult>.Success(result, "Sipariş başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<OrderQueryResult>.Failure("Sipariş getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

