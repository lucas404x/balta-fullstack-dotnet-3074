namespace Dima.Core.Entities;

public abstract class BaseEntity
{
    public long Seq { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
}