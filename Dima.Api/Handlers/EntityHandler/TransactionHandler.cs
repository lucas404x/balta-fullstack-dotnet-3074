using Dima.Api.Data.Repositories;
using Dima.Core.Entities;

namespace Dima.Api.Handlers.EntityHandler;

public class TransactionHandler(IEntityRepository<Transaction> repository) : BaseEntityHandler<Transaction>(repository)
{
}
