using FluentValidation;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderDetailCommands;

namespace OnionVb02.ValidatorStructor.Validators.OrderDetailValidators
{
    public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Geçerli bir sipariş ID'si girilmelidir");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Geçerli bir ürün ID'si girilmelidir");
        }
    }
}

