namespace Dima.Core.Requests.Transaction;

public record GetTransactionsByPeriodRequest(
    DateTime? StartDate, DateTime? EndDate, string UserId, int PageNumber, int PageSize) : PagedRequest(UserId, PageNumber, PageSize), IRequestValidate
{
    public string? Validate()
    {
        if (StartDate > EndDate) return "Start Date must be less than End Date.";
        return null;
    }
}
