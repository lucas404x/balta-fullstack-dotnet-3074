namespace Dima.Api.Core.Abstractions;

internal interface IEndpointGroup
{
    static abstract void Map(IEndpointRouteBuilder app);
}

