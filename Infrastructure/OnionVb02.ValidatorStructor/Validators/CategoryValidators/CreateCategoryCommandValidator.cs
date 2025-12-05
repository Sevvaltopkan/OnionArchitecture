using FluentValidation;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.CategoryCommands;

namespace OnionVb02.ValidatorStructor.Validators.CategoryValidators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");
        }
    }
}

