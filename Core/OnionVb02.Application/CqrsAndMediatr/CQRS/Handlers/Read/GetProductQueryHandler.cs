using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.ProductQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.ProductResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetProductQueryHandler : IQueryHandler<GetAllProductsQuery, List<ProductQueryResult>>
    {
        private readonly IProductRepository _repository;

        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ProductQueryResult>>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<Product> values = await _repository.GetAllAsync();

                var result = values.Select(x => new ProductQueryResult
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category?.CategoryName
                }).ToList();

                return Result<List<ProductQueryResult>>.Success(result, "Ürünler başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<List<ProductQueryResult>>.Failure("Ürünler getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

