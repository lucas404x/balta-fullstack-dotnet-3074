namespace Dima.Core.Requests;

public class PagedRequest : BaseRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}