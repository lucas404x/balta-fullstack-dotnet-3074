
using Dima.Api.Filters;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.EntityEndpoints;

abstract internal class BaseEntityEndpoint<TEntity, THandler> : IEndpointGroup 
    where TEntity : BaseEntity
    where THandler : IEntityHandler<TEntity>
{
    private static readonly string _tableName = typeof(TEntity).Name;

    public static void Map(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(_tableName.ToLower())
            .WithSummary(_tableName)
            .WithTags(_tableName);

        mapGroup.MapPost("/get-all", HandleGetAll)
            .WithDescription($"Get all registers from {_tableName} using pagination.");

        mapGroup.MapPost("/", HandleCreate)
            .AddEndpointFilter<EntityValidationFilter<TEntity>>()
            .WithDescription($"Create a brand new register into {_tableName}.");

        mapGroup.MapPut("/", HandleUpdate)
            .AddEndpointFilter<EntityValidationFilter<TEntity>>()
            .WithDescription($"Update effeciently an existing register in the {_tableName}.");
        
        mapGroup.MapDelete("/{seq}", HandleDelete)
            .WithDescription($"Delete an register from the table {_tableName}.");

        mapGroup.MapGet("/{seq}", HandleGetBySeq)
            .WithDescription($"Retrieves a {_tableName} register by Seq.");
    }

    private static async Task<IResult> HandleGetAll(
        GetAllRequest<TEntity> request,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.GetAll(request));

    private static async Task<IResult> HandleCreate(
        CreateRequest<TEntity> request,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.Create(request));

    private static async Task<IResult> HandleUpdate(
        UpdateRequest<TEntity> request,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.Update(request));

    private static async Task<IResult> HandleDelete(
        long seq,
        [FromHeader] string UserId,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.Delete(new(UserId, seq)));

    private static async Task<IResult> HandleGetBySeq(
        long seq,
        [FromHeader] string UserId,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.GetBySeq(new(UserId, seq)));
}
