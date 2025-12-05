using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.AppUserQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.Read
{
    public class GetAppUserQueryHandler : IQueryHandler<GetAppUserQuery, List<GetAppUserQueryResult>>
    {
        private readonly IAppUserRepository _repository;

        public GetAppUserQueryHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetAppUserQueryResult>>> Handle(
            GetAppUserQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                List<AppUser> values = await _repository.GetAllAsync();

                var result = values.Select(x => new GetAppUserQueryResult
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password
                }).ToList();

                return Result<List<GetAppUserQueryResult>>.Success(result, "Kullanıcılar başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<List<GetAppUserQueryResult>>.Failure("Kullanıcılar getirilirken hata oluştu", ex.Message);
            }
        }
    }
}
