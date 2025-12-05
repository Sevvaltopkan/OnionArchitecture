using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderDetailCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderDetailResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class UpdateOrderDetailCommandHandler : ICommandHandler<UpdateOrderDetailCommand, OrderDetailQueryResult>
    {
        private readonly IOrderDetailRepository _repository;

        public UpdateOrderDetailCommandHandler(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDetailQueryResult>> Handle(
            UpdateOrderDetailCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                OrderDetail value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result<OrderDetailQueryResult>.Failure($"ID: {command.Id} bulunamadı");

                value.OrderId = command.OrderId;
                value.ProductId = command.ProductId;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;

                await _repository.SaveChangesAsync();

                var dto = new OrderDetailQueryResult
                {
                    Id = value.Id,
                    OrderId = value.OrderId,
                    ProductId = value.ProductId
                };

                return Result<OrderDetailQueryResult>.Success(dto, "Sipariş detayı başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return Result<OrderDetailQueryResult>.Failure("Sipariş detayı güncellenirken hata oluştu", ex.Message);
            }
        }
    }
}

