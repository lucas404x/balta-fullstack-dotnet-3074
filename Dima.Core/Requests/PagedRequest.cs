using Dima.Core.Abstractions;

namespace Dima.Core.Requests;

public class PagedRequest : BaseRequest, IRequestValidate
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    
    public virtual string? Validate()
    {
        if (PageNumber < 1) return "Page number must be greater than 0.";
        if (PageSize < 1) return "Page size must be greater than 0.";
        return null;
    }
}