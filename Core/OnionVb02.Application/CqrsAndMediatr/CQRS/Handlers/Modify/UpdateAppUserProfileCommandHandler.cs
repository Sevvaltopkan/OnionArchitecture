using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.AppUserProfileCommands;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.AppUserProfileResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class UpdateAppUserProfileCommandHandler : ICommandHandler<UpdateAppUserProfileCommand, AppUserProfileQueryResult>
    {
        private readonly IAppUserProfileRepository _repository;

        public UpdateAppUserProfileCommandHandler(IAppUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<AppUserProfileQueryResult>> Handle(
            UpdateAppUserProfileCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                AppUserProfile value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result<AppUserProfileQueryResult>.Failure($"ID: {command.Id} bulunamadı");

                value.FirstName = command.FirstName;
                value.LastName = command.LastName;
                value.AppUserId = command.AppUserId;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;

                await _repository.SaveChangesAsync();

                var dto = new AppUserProfileQueryResult
                {
                    Id = value.Id,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    AppUserId = value.AppUserId
                };

                return Result<AppUserProfileQueryResult>.Success(dto, "Kullanıcı profili başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return Result<AppUserProfileQueryResult>.Failure("Kullanıcı profili güncellenirken hata oluştu", ex.Message);
            }
        }
    }
}

