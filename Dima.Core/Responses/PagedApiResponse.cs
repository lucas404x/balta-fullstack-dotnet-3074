using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class PagedApiResponse<T> : ApiResponse<T>
{
    public PagedApiResponse(string errorMessage) : base(errorMessage)
    {
        
    }
    
    [JsonConstructor]
    public PagedApiResponse(
        T result, int currentPage, int totalRecords, int pageSize = Configuration.DefaultPageSize) : base(result)
    {
        CurrentPage = currentPage;
        TotalRecords = totalRecords;
        PageSize = pageSize;
    }
    
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages  => (int)Math.Ceiling((double)TotalRecords / PageSize);
}