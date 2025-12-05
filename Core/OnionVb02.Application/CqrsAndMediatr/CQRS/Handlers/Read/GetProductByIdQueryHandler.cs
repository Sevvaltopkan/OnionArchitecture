using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.ProductQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.ProductResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductQueryResult>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductQueryResult>> Handle(
            GetProductByIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                Product value = await _repository.GetByIdAsync(query.Id);

                if (value == null)
                    return Result<ProductQueryResult>.Failure($"ID: {query.Id} bulunamadı");

                var result = new ProductQueryResult
                {
                    Id = value.Id,
                    ProductName = value.ProductName,
                    UnitPrice = value.UnitPrice,
                    CategoryId = value.CategoryId,
                    CategoryName = value.Category?.CategoryName
                };

                return Result<ProductQueryResult>.Success(result, "Ürün başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<ProductQueryResult>.Failure("Ürün getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

