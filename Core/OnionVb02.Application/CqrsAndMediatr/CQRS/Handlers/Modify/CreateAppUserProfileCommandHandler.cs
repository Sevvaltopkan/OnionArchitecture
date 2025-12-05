using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.AppUserProfileCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.AppUserProfileResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class CreateAppUserProfileCommandHandler : ICommandHandler<CreateAppUserProfileCommand, AppUserProfileQueryResult>
    {
        private readonly IAppUserProfileRepository _repository;

        public CreateAppUserProfileCommandHandler(IAppUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<AppUserProfileQueryResult>> Handle(
            CreateAppUserProfileCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = new AppUserProfile
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    AppUserId = command.AppUserId,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(entity);

                var dto = new AppUserProfileQueryResult
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    AppUserId = entity.AppUserId
                };

                return Result<AppUserProfileQueryResult>.Success(dto, "Kullanıcı profili başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return Result<AppUserProfileQueryResult>.Failure("Kullanıcı profili oluşturulurken hata oluştu", ex.Message);
            }
        }
    }
}

