using System.Net;
using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class PagedApiResponse<T> : ApiResponse<T>
{
    [JsonConstructor]
    public PagedApiResponse(
        T result,
        string message,
        int currentPage, 
        int totalRecords, 
        int pageSize = Configuration.DefaultPageSize) : base(result, message)
    {
        CurrentPage = currentPage;
        TotalRecords = totalRecords;
        PageSize = pageSize;
    }

    public PagedApiResponse(
        string errorMessage, 
        HttpStatusCode code = HttpStatusCode.InternalServerError) : base(errorMessage, code)
    {
        
    }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; } = Configuration.DefaultPageSize;
    public int TotalPages 
        => TotalRecords == 0 ? 0 : (int)Math.Ceiling((double)TotalRecords / PageSize);
}