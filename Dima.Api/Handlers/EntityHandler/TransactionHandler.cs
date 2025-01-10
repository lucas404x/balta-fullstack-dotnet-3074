using Dima.Api.Data.Repositories;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;

namespace Dima.Api.Handlers.EntityHandler;

public class TransactionHandler(IEntityRepository<Transaction> repository) : BaseEntityHandler<Transaction>(repository), ITransactionHandler
{
}
