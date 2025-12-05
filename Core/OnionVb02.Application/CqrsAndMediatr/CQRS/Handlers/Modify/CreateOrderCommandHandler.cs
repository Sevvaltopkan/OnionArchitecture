using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderQueryResult>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderQueryResult>> Handle(
            CreateOrderCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Order
                {
                    ShippingAddress = command.ShippingAddress,
                    AppUserId = command.AppUserId,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(entity);

                var dto = new OrderQueryResult
                {
                    Id = entity.Id,
                    ShippingAddress = entity.ShippingAddress,
                    AppUserId = entity.AppUserId,
                    CreatedDate = entity.CreatedDate
                };

                return Result<OrderQueryResult>.Success(dto, "Sipariş başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return Result<OrderQueryResult>.Failure("Sipariş oluşturulurken hata oluştu", ex.Message);
            }
        }
    }
}

