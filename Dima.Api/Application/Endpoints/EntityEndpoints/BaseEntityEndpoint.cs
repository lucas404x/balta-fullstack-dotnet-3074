using Dima.Api.Application.Filters;
using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Application.Endpoints.EntityEndpoints;

abstract internal class BaseEntityEndpoint<TEntity, THandler> : IEndpointGroup
    where TEntity : BaseEntity
    where THandler : IEntityHandler<TEntity>
{
    private static readonly string _tableName = typeof(TEntity).Name;

    public static RouteGroupBuilder Map(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(_tableName.ToLower())
            .WithSummary(_tableName)
            .WithTags(_tableName);

        mapGroup.MapPost("/get-all", HandleGetAll)
            .WithDescription($"Get all registers from {_tableName} using pagination.");

        mapGroup.MapPost("/", HandleCreate)
            .WithDescription($"Create a brand new register into {_tableName}.")
            .AddEndpointFilter<EntityValidationFilter<TEntity>>();

        mapGroup.MapPut("/", HandleUpdate)
            .WithDescription($"Update effeciently an existing register in the {_tableName}.")
            .AddEndpointFilter<EntityValidationFilter<TEntity>>();
        
        mapGroup.MapDelete("/{seq}", HandleDelete)
            .WithDescription($"Delete an register from the table {_tableName}.");

        mapGroup.MapGet("/{seq}", HandleGetBySeq)
            .WithDescription($"Retrieves a {_tableName} register by Seq.");

        return mapGroup;
    }

    private static async Task<IValueHttpResult<PagedApiResponse<List<TEntity>>>> HandleGetAll(
        GetAllRequest<TEntity> request,
        THandler entityHandler,
        CancellationToken cancellationToken) 
        => TypedResults.Ok(await entityHandler.GetAll(request, cancellationToken));

    private static async Task<IValueHttpResult<ApiResponse<TEntity>>> HandleCreate(
        CreateRequest<TEntity> request,
        THandler entityHandler,
        CancellationToken cancellationToken)
        => TypedResults.Ok(await entityHandler.Create(request, cancellationToken));

    private static async Task<IValueHttpResult<ApiResponse<TEntity>>> HandleUpdate(
        UpdateRequest<TEntity> request,
        THandler entityHandler,
        CancellationToken cancellationToken)
        => TypedResults.Ok(await entityHandler.Update(request, cancellationToken));

    private static async Task<IValueHttpResult<ApiResponse<bool>>> HandleDelete(
        long seq,
        [FromHeader] string userId,
        THandler entityHandler,
        CancellationToken cancellationToken)
        => TypedResults.Ok(await entityHandler.Delete(new(userId, seq), cancellationToken));

    private static async Task<IValueHttpResult<ApiResponse<TEntity>>> HandleGetBySeq(
        long seq,
        [FromHeader] string userId,
        THandler entityHandler,
        CancellationToken cancellationToken)
        => TypedResults.Ok(await entityHandler.GetBySeq(new(userId, seq), cancellationToken));
}
