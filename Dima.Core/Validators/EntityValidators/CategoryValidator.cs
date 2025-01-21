using Dima.Core.Entities;
using FluentValidation;

namespace Dima.Core.Validators.EntityValidators;

public sealed class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        Include(new BaseEntityValidator());
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("O título não pode estar vazio.")
            .MaximumLength(80)
            .WithMessage("O título deve conter até 80 caracteres.");
        RuleFor(x => x.Description).MaximumLength(300);
    }
}