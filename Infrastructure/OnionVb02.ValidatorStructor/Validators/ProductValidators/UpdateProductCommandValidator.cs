using FluentValidation;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.ProductCommands;

namespace OnionVb02.ValidatorStructor.Validators.ProductValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş olamaz")
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır")
                .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Birim fiyatı 0'dan büyük olmalıdır");
        }
    }
}

