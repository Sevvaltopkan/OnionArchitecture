using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.AppUserProfileCommands;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Handlers.Modify
{
    public class RemoveAppUserProfileCommandHandler : ICommandHandler<RemoveAppUserProfileCommand>
    {
        private readonly IAppUserProfileRepository _repository;

        public RemoveAppUserProfileCommandHandler(IAppUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RemoveAppUserProfileCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AppUserProfile value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result.Failure($"ID: {command.Id} bulunamadı");

                await _repository.DeleteAsync(value);
                return Result.Success("Kullanıcı profili başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Result.Failure("Kullanıcı profili silinirken hata oluştu", ex.Message);
            }
        }
    }
}

