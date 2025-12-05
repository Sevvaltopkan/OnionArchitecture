using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, OrderQueryResult>
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderQueryResult>> Handle(
            UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                Order value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result<OrderQueryResult>.Failure($"ID: {command.Id} bulunamadı");

                value.ShippingAddress = command.ShippingAddress;
                value.AppUserId = command.AppUserId;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;

                await _repository.SaveChangesAsync();

                var dto = new OrderQueryResult
                {
                    Id = value.Id,
                    ShippingAddress = value.ShippingAddress,
                    AppUserId = value.AppUserId,
                    CreatedDate = value.CreatedDate
                };

                return Result<OrderQueryResult>.Success(dto, "Sipariş başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return Result<OrderQueryResult>.Failure("Sipariş güncellenirken hata oluştu", ex.Message);
            }
        }
    }
}

