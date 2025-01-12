namespace Dima.Api.Domain.Abstractions;

internal interface IEndpointGroup
{
    static abstract RouteGroupBuilder Map(IEndpointRouteBuilder app);
}

