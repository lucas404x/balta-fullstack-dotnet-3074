
namespace Dima.Api.Endpoints;

internal class HomeEndpoint : IEndpointGroup
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGroup("/")
            .MapGet("health-check", () => TypedResults.Ok(":p"))
            .WithTags("/");
    }
}
