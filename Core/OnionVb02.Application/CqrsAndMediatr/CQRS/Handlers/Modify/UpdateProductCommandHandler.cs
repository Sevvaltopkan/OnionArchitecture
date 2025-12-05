using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.ProductCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.ProductResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ProductQueryResult>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductQueryResult>> Handle(
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                Product value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result<ProductQueryResult>.Failure($"ID: {command.Id} bulunamadı");

                value.ProductName = command.ProductName;
                value.UnitPrice = command.UnitPrice;
                value.CategoryId = command.CategoryId;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;

                await _repository.SaveChangesAsync();

                var dto = new ProductQueryResult
                {
                    Id = value.Id,
                    ProductName = value.ProductName,
                    UnitPrice = value.UnitPrice,
                    CategoryId = value.CategoryId
                };

                return Result<ProductQueryResult>.Success(dto, "Ürün başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return Result<ProductQueryResult>.Failure("Ürün güncellenirken hata oluştu", ex.Message);
            }
        }
    }
}

