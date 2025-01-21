using Dima.Core.Extensions;
using Dima.Core.Validators.EntityValidators;

namespace Dima.Core.Entities;

public sealed class Category : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public override List<string> Validate() 
        => new CategoryValidator().Validate(this).GetErrorMessages();
}