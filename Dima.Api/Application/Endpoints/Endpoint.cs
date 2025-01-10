using Dima.Api.Application.Endpoints.EntityEndpoints;
using Dima.Api.Domain.Abstractions;

namespace Dima.Api.Application.Endpoints;

internal static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapEndpoint<HomeEndpoint>();
        app.MapEndpoint<CategoryEndpoint>();
        app.MapEndpoint<TransactionEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpointGroup
    {
        TEndpoint.Map(app);
        return app;
    }
}
