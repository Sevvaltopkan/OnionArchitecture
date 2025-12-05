using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.AppUserQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.Read
{
    public class GetAppUserByIdQueryHandler : IQueryHandler<GetAppUserByIdQuery, GetAppUserByIdQueryResult>
    {
        private readonly IAppUserRepository _repository;

        public GetAppUserByIdQueryHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetAppUserByIdQueryResult>> Handle(
            GetAppUserByIdQuery query,
            CancellationToken cancellationToken)
        {
            try
            {
                AppUser value = await _repository.GetByIdAsync(query.Id);

                if (value == null)
                    return Result<GetAppUserByIdQueryResult>.Failure($"ID: {query.Id} bulunamadı");

                var result = new GetAppUserByIdQueryResult
                {
                    Id = value.Id,
                    UserName = value.UserName,
                    Password = value.Password
                };

                return Result<GetAppUserByIdQueryResult>.Success(result, "Kullanıcı başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return Result<GetAppUserByIdQueryResult>.Failure("Kullanıcı getirilirken hata oluştu", ex.Message);
            }
        }
    }
}
