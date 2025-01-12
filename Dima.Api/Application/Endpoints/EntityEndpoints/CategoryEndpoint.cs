using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;

namespace Dima.Api.Application.Endpoints.EntityEndpoints;

internal class CategoryEndpoint : IEndpointGroup
{
    public static RouteGroupBuilder Map(IEndpointRouteBuilder app)
        => BaseEntityEndpoint<Category, ICategoryHandler>.Map(app);
}
