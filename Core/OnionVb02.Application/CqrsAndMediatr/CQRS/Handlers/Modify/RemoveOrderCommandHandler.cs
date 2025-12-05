using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderCommands;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class RemoveOrderCommandHandler : ICommandHandler<RemoveOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public RemoveOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RemoveOrderCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Order value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result.Failure($"ID: {command.Id} bulunamadı");

                await _repository.DeleteAsync(value);
                return Result.Success("Sipariş başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Result.Failure("Sipariş silinirken hata oluştu", ex.Message);
            }
        }
    }
}

