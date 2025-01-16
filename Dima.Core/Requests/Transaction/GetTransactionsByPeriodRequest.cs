using Dima.Core.Abstractions;

namespace Dima.Core.Requests.Transaction;

public class GetTransactionsByPeriodRequest : PagedRequest, IRequestValidate
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string? Validate()
    {
        if ((StartDate is null && EndDate is not null) || (StartDate is not null && EndDate is null))
            return "One date was provided but the another wasn't. You must provide both or any of them.";
        else if (StartDate > EndDate) 
            return "Start Date must be less than End Date.";
        return null;
    }
}
