using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.OrderQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetOrderQueryHandler : IQueryHandler<GetAllOrdersQuery, List<OrderQueryResult>>
    {
        private readonly IOrderRepository _repository;

        public GetOrderQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<OrderQueryResult>>> Handle(
            GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<Order> values = await _repository.GetAllAsync();

                var result = values.Select(x => new OrderQueryResult
                {
                    Id = x.Id,
                    ShippingAddress = x.ShippingAddress,
                    AppUserId = x.AppUserId,
                    UserName = x.AppUser?.UserName,
                    CreatedDate = x.CreatedDate
                }).ToList();

                return Result<List<OrderQueryResult>>.Success(result, "Siparişler başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<List<OrderQueryResult>>.Failure("Siparişler getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

