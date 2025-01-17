using Dima.Core.Entities;
using FluentValidation;

namespace Dima.Core.Validators;

public sealed class BaseEntityValidator : AbstractValidator<BaseEntity>
{
    public BaseEntityValidator()
    {
        RuleFor(x => x.CreatedDate).NotNull();
    }
}
