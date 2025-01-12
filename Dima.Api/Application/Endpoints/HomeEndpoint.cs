using Dima.Api.Domain.Abstractions;

namespace Dima.Api.Application.Endpoints;

internal class HomeEndpoint : IEndpointGroup
{
    public static RouteGroupBuilder Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/");
        
        group.MapGet("health-check", () => TypedResults.Ok(":p"))
            .WithTags("/");
        
        return group;
    }
}
