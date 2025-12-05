using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.ProductCommands;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand>
    {
        private readonly IProductRepository _repository;

        public RemoveProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Product value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result.Failure($"ID: {command.Id} bulunamadı");

                await _repository.DeleteAsync(value);
                return Result.Success("Ürün başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Result.Failure("Ürün silinirken hata oluştu", ex.Message);
            }
        }
    }
}

