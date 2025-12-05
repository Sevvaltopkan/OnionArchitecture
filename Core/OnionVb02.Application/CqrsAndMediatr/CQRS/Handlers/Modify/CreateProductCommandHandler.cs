using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.ProductCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.ProductResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductQueryResult>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductQueryResult>> Handle(
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Product
                {
                    ProductName = command.ProductName,
                    UnitPrice = command.UnitPrice,
                    CategoryId = command.CategoryId,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(entity);

                var dto = new ProductQueryResult
                {
                    Id = entity.Id,
                    ProductName = entity.ProductName,
                    UnitPrice = entity.UnitPrice,
                    CategoryId = entity.CategoryId
                };

                return Result<ProductQueryResult>.Success(dto, "Ürün başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return Result<ProductQueryResult>.Failure("Ürün oluşturulurken hata oluştu", ex.Message);
            }
        }
    }
}

