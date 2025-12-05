using FluentValidation;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.AppUserProfileCommands;

namespace OnionVb02.ValidatorStructor.Validators.AppUserProfileValidators
{
    public class UpdateAppUserProfileCommandValidator : AbstractValidator<UpdateAppUserProfileCommand>
    {
        public UpdateAppUserProfileCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID'si girilmelidir");
        }
    }
}

