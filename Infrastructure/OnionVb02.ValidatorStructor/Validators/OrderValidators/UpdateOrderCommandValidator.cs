using FluentValidation;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderCommands;

namespace OnionVb02.ValidatorStructor.Validators.OrderValidators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("Teslimat adresi boş olamaz")
                .MinimumLength(10).WithMessage("Teslimat adresi en az 10 karakter olmalıdır")
                .MaximumLength(500).WithMessage("Teslimat adresi en fazla 500 karakter olabilir");
        }
    }
}

