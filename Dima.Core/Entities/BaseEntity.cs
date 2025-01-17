using FluentValidation;

namespace Dima.Core.Entities;

public abstract class BaseEntity
{   
    public long Seq { get; set; }
    public string? UserId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }

    public abstract List<string> Validate();
}