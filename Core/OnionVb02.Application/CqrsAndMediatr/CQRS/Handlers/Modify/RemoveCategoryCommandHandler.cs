using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.CategoryCommands;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class RemoveCategoryCommandHandler : ICommandHandler<RemoveCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        public RemoveCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RemoveCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Category value = await _repository.GetByIdAsync(command.Id);
                
                if (value == null)
                    return Result.Failure($"ID: {command.Id} bulunamadı");

                await _repository.DeleteAsync(value);
                return Result.Success("Category başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Result.Failure("Category silinirken hata oluştu", ex.Message);
            }
        }
    }
}
