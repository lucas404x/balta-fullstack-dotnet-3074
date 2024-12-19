namespace Dima.Core.Requests;

public record PagedRequest(
    string UserId, int PageNumber, int PageSize = Configuration.DefaultPageSize) : BaseRequest(UserId);