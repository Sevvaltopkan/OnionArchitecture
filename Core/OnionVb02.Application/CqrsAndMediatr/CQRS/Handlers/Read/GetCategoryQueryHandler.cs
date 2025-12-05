using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.CategoryQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.CategoryResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetCategoryQueryHandler : IQueryHandler<GetAllCategoriesQuery, List<GetCategoryQueryResult>>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetCategoryQueryResult>>> Handle(
            GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<Category> values = await _repository.GetAllAsync();

                var result = values.Select(x => new GetCategoryQueryResult
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description
                }).ToList();

                return Result<List<GetCategoryQueryResult>>.Success(result, "Kategoriler başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<List<GetCategoryQueryResult>>.Failure("Kategoriler getirilirken hata oluştu", ex.Message);
            }
        }
    }
}
