using Dima.Api.Data.Repositories;
using Dima.Core.Entities;

namespace Dima.Api.Handlers.EntityHandler;

public class CategoryHandler(IEntityRepository<Category> repository) : BaseEntityHandler<Category>(repository)
{
}
