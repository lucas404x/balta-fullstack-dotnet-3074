using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;

namespace Dima.Api.Application.Handlers.EntityHandler;

public class CategoryHandler(IEntityRepository<Category> repository) : BaseEntityHandler<Category>(repository), ICategoryHandler
{
}
