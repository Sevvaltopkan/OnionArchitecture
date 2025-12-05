using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderDetailCommands;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class RemoveOrderDetailCommandHandler : ICommandHandler<RemoveOrderDetailCommand>
    {
        private readonly IOrderDetailRepository _repository;

        public RemoveOrderDetailCommandHandler(IOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RemoveOrderDetailCommand command, CancellationToken cancellationToken)
        {
            try
            {
                OrderDetail value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result.Failure($"ID: {command.Id} bulunamadı");

                await _repository.DeleteAsync(value);
                return Result.Success("Sipariş detayı başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Result.Failure("Sipariş detayı silinirken hata oluştu", ex.Message);
            }
        }
    }
}

