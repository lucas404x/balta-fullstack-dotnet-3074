using Dima.Api.Data;
using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Extensions;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests.Transaction;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Application.Handlers.EntityHandler;

public class TransactionHandler(
    AppDbContext context, IEntityRepository<Transaction> repository) : BaseEntityHandler<Transaction>(repository), ITransactionHandler
{
    public async Task<PagedApiResponse<List<Transaction>>> GetByPeriod(GetTransactionsByPeriodRequest request, CancellationToken cancellationToken = default)
    {
        DateTime startDate = request.StartDate ?? DateTime.Now.GetFirstDayOfMonth();
        DateTime endDate = request.EndDate ?? DateTime.Now.GetLastDayOfMonth();

        var query = context
            .Transactions
            .AsNoTracking()
            .Where(t => t.CreatedDate >= startDate && t.CreatedDate <= endDate)
            .OrderByDescending(t => t.Title);

        var count = await query.CountAsync(cancellationToken: cancellationToken);
        if (count == 0) 
            return PagedApiResponse<List<Transaction>>.Empty("Transações recuperados com sucesso!");

        var values = await query
            .Skip(request.PageSize)
            .Take(request.PageSize * (request.PageNumber - 1))
            .ToListAsync(cancellationToken: cancellationToken);

        return new(values, "Transações recuperados com sucesso!", request.PageNumber, count, request.PageSize);
    }
}
