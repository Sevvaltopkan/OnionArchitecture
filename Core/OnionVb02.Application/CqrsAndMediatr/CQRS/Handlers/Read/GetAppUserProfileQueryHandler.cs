using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.AppUserProfileQueries;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.AppUserProfileResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Read
{
    public class GetAppUserProfileQueryHandler : IQueryHandler<GetAllAppUserProfilesQuery, List<AppUserProfileQueryResult>>
    {
        private readonly IAppUserProfileRepository _repository;

        public GetAppUserProfileQueryHandler(IAppUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<AppUserProfileQueryResult>>> Handle(
            GetAllAppUserProfilesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<AppUserProfile> values = await _repository.GetAllAsync();

                var result = values.Select(x => new AppUserProfileQueryResult
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AppUserId = x.AppUserId,
                    UserName = x.AppUser?.UserName
                }).ToList();

                return Result<List<AppUserProfileQueryResult>>.Success(result, "Kullanıcı profilleri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<List<AppUserProfileQueryResult>>.Failure("Kullanıcı profilleri getirilirken hata oluştu", ex.Message);
            }
        }
    }
}

