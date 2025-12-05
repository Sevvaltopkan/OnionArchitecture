using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.Modify
{
    public class UpdateAppUserCommandHandler : ICommandHandler<UpdateAppUserCommand, GetAppUserByIdQueryResult>
    {
        private readonly IAppUserRepository _repository;

        public UpdateAppUserCommandHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetAppUserByIdQueryResult>> Handle(
            UpdateAppUserCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                AppUser value = await _repository.GetByIdAsync(command.Id);

                if (value == null)
                    return Result<GetAppUserByIdQueryResult>.Failure($"ID: {command.Id} bulunamadı");

                value.UserName = command.UserName;
                value.Password = command.Password;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;

                await _repository.SaveChangesAsync();

                var dto = new GetAppUserByIdQueryResult
                {
                    Id = value.Id,
                    UserName = value.UserName,
                    Password = value.Password
                };

                return Result<GetAppUserByIdQueryResult>.Success(dto, "Kullanıcı başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return Result<GetAppUserByIdQueryResult>.Failure("Kullanıcı güncellenirken hata oluştu", ex.Message);
            }
        }
    }
}
