namespace Dima.Api.Domain.Abstractions;

internal interface IEndpointGroup
{
    static abstract void Map(IEndpointRouteBuilder app);
}

