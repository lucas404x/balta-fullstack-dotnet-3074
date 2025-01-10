using Dima.Api.Core.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;

namespace Dima.Api.Handlers.EntityHandler;

public class CategoryHandler(IEntityRepository<Category> repository) : BaseEntityHandler<Category>(repository), ICategoryHandler
{
}
