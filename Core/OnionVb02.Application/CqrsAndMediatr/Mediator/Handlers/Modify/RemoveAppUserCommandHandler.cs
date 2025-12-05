using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.Modify
{
    public class RemoveAppUserCommandHandler : ICommandHandler<RemoveAppUserCommand>
    {
        private readonly IAppUserRepository _repository;

        public RemoveAppUserCommandHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(RemoveAppUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AppUser value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result.Failure($"ID: {command.Id} bulunamadı");

                await _repository.DeleteAsync(value);
                return Result.Success("Kullanıcı başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Result.Failure("Kullanıcı silinirken hata oluştu", ex.Message);
            }
        }
    }
}
