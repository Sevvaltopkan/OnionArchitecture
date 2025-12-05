using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.CategoryCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.CategoryResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, GetCategoryByIdQueryResult>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetCategoryByIdQueryResult>> Handle(
            CreateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Category
                {
                    CategoryName = command.CategoryName,
                    Description = command.Description,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(entity);

                var dto = new GetCategoryByIdQueryResult
                {
                    Id = entity.Id,
                    CategoryName = entity.CategoryName,
                    Description = entity.Description
                };

                return Result<GetCategoryByIdQueryResult>.Success(dto, "Category başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return Result<GetCategoryByIdQueryResult>.Failure("Category oluşturulurken hata oluştu", ex.Message);
            }
        }
    }
}
