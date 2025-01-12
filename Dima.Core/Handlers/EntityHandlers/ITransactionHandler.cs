using Dima.Core.Entities;
using Dima.Core.Requests.Transaction;
using Dima.Core.Responses;

namespace Dima.Core.Handlers.EntityHandlers;

public interface ITransactionHandler : IEntityHandler<Transaction>
{
    Task<PagedApiResponse<List<Transaction>>> GetByPeriod(GetTransactionsByPeriodRequest request, CancellationToken cancellationToken = default);
}
