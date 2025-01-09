using FluentValidation;

namespace Dima.Core.Entities;

public abstract class BaseEntity
{   
    #region Validation
    
    internal class BaseEntityValidator : AbstractValidator<BaseEntity>
    {
        internal BaseEntityValidator()
        {
            RuleFor(x => x.CreatedDate).NotNull();
        }
    }
    
    #endregion
    
    public long Seq { get; set; }
    public string? UserId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    public abstract List<string> Validate();
    
    public virtual Task<List<string>> ValidateAsync()
     => Task.FromResult(Validate());
}