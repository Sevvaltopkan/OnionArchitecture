using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.Modify
{
    public class CreateAppUserCommandHandler : ICommandHandler<CreateAppUserCommand, GetAppUserByIdQueryResult>
    {
        private readonly IAppUserRepository _repository;

        public CreateAppUserCommandHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetAppUserByIdQueryResult>> Handle(
            CreateAppUserCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var entity = new AppUser
                {
                    UserName = command.UserName,
                    Password = command.Password,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(entity);

                var dto = new GetAppUserByIdQueryResult
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    Password = entity.Password
                };

                return Result<GetAppUserByIdQueryResult>.Success(dto, "Kullanıcı başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return Result<GetAppUserByIdQueryResult>.Failure("Kullanıcı oluşturulurken hata oluştu", ex.Message);
            }
        }
    }
}
