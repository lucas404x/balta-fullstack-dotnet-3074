using Dima.Core.Extensions;
using FluentValidation;

namespace Dima.Core.Entities;

public sealed class Category : BaseEntity
{
    #region Validation
    
    private readonly CategoryValidator _validator = new();
    
    internal class CategoryValidator : AbstractValidator<Category>
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
    
    #endregion
    
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public override List<string> Validate() => _validator.Validate(this).GetErrorMessages();
}