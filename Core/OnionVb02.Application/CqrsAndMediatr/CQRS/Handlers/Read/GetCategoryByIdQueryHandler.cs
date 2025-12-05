using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.CategoryQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.CategoryResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResult>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetCategoryByIdQueryResult>> Handle(
            GetCategoryByIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                Category value = await _repository.GetByIdAsync(query.Id);

                if (value == null)
                    return Result<GetCategoryByIdQueryResult>.Failure($"ID: {query.Id} bulunamadı");

                var result = new GetCategoryByIdQueryResult
                {
                    Id = value.Id,
                    CategoryName = value.CategoryName,
                    Description = value.Description
                };

                return Result<GetCategoryByIdQueryResult>.Success(result, "Kategori başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<GetCategoryByIdQueryResult>.Failure("Kategori getirilirken hata oluştu", ex.Message);
            }
        }
    }
}
