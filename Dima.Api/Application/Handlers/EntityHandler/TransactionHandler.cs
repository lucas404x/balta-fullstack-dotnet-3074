using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;

namespace Dima.Api.Application.Handlers.EntityHandler;

public class TransactionHandler(IEntityRepository<Transaction> repository) : BaseEntityHandler<Transaction>(repository), ITransactionHandler
{
}
