using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.AppUserProfileQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.AppUserProfileResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetAppUserProfileByIdQueryHandler : IQueryHandler<GetAppUserProfileByIdQuery, AppUserProfileQueryResult>
    {
        private readonly IAppUserProfileRepository _repository;

        public GetAppUserProfileByIdQueryHandler(IAppUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<AppUserProfileQueryResult>> Handle(
            GetAppUserProfileByIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                AppUserProfile value = await _repository.GetByIdAsync(query.Id);

                if (value == null)
                    return Result<AppUserProfileQueryResult>.Failure($"ID: {query.Id} bulunamadı");

                var result = new AppUserProfileQueryResult
                {
                    Id = value.Id,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    AppUserId = value.AppUserId,
                    UserName = value.AppUser?.UserName
                };

                return Result<AppUserProfileQueryResult>.Success(result, "Kullanıcı profili başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<AppUserProfileQueryResult>.Failure("Kullanıcı profili getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

