using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderDetailCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderDetailResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class CreateOrderDetailCommandHandler : ICommandHandler<CreateOrderDetailCommand, OrderDetailQueryResult>
    {
        private readonly IOrderDetailRepository _repository;

        public CreateOrderDetailCommandHandler(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDetailQueryResult>> Handle(
            CreateOrderDetailCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = new OrderDetail
                {
                    OrderId = command.OrderId,
                    ProductId = command.ProductId,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(entity);

                var dto = new OrderDetailQueryResult
                {
                    Id = entity.Id,
                    OrderId = entity.OrderId,
                    ProductId = entity.ProductId
                };

                return Result<OrderDetailQueryResult>.Success(dto, "Sipariş detayı başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return Result<OrderDetailQueryResult>.Failure("Sipariş detayı oluşturulurken hata oluştu", ex.Message);
            }
        }
    }
}

