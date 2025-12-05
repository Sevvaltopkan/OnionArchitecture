using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.CategoryCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.CategoryResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, GetCategoryByIdQueryResult>
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetCategoryByIdQueryResult>> Handle(
            UpdateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                Category value = await _repository.GetByIdAsync(command.Id);
                
                if (value == null)
                    return Result<GetCategoryByIdQueryResult>.Failure($"ID: {command.Id} bulunamadı");

                value.CategoryName = command.CategoryName;
                value.Description = command.Description;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;
                
                await _repository.SaveChangesAsync();

                var dto = new GetCategoryByIdQueryResult
                {
                    Id = value.Id,
                    CategoryName = value.CategoryName,
                    Description = value.Description
                };

                return Result<GetCategoryByIdQueryResult>.Success(dto, "Category başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return Result<GetCategoryByIdQueryResult>.Failure("Category güncellenirken hata oluştu", ex.Message);
            }
        }
    }
}
