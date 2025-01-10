using Dima.Api.Application.Filters;
using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dima.Api.Application.Endpoints.EntityEndpoints;

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
            .WithDescription($"Get all registers from {_tableName} using pagination.")
            .Produces<PagedApiResponse<List<TEntity>>>()
            .Produces<PagedApiResponse<List<TEntity>>>(StatusCodes.Status400BadRequest)
            .Produces<PagedApiResponse<List<TEntity>>>(StatusCodes.Status500InternalServerError);

        mapGroup.MapPost("/", HandleCreate)
            .AddEndpointFilter<EntityValidationFilter<TEntity>>()
            .WithDescription($"Create a brand new register into {_tableName}.")
            .Produces<ApiResponse<TEntity>>()
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status422UnprocessableEntity)
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status500InternalServerError);

        mapGroup.MapPut("/", HandleUpdate)
            .AddEndpointFilter<EntityValidationFilter<TEntity>>()
            .WithDescription($"Update effeciently an existing register in the {_tableName}.")
            .Produces<ApiResponse<TEntity>>()
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status422UnprocessableEntity)
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status500InternalServerError);

        mapGroup.MapDelete("/{seq}", HandleDelete)
            .WithDescription($"Delete an register from the table {_tableName}.")
            .Produces<ApiResponse<bool>>()
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status500InternalServerError);

        mapGroup.MapGet("/{seq}", HandleGetBySeq)
            .WithDescription($"Retrieves a {_tableName} register by Seq.")
            .Produces<ApiResponse<TEntity>>()
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status404NotFound)
            .Produces<ApiResponse<TEntity>>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IValueHttpResult<PagedApiResponse<List<TEntity>>>> HandleGetAll(
        GetAllRequest<TEntity> request,
        THandler entityHandler)
    {
        string? requestValidateMsg = request.Validate();
        if (!string.IsNullOrWhiteSpace(requestValidateMsg))
        {
            return TypedResults.BadRequest(new PagedApiResponse<List<TEntity>>(requestValidateMsg, HttpStatusCode.BadRequest));
        }
        return TypedResults.Ok(await entityHandler.GetAll(request));
    }

    private static async Task<IValueHttpResult<ApiResponse<TEntity>>> HandleCreate(
        CreateRequest<TEntity> request,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.Create(request));

    private static async Task<IValueHttpResult<ApiResponse<TEntity>>> HandleUpdate(
        UpdateRequest<TEntity> request,
        THandler entityHandler)
        => TypedResults.Ok(await entityHandler.Update(request));

    private static async Task<IValueHttpResult<ApiResponse<bool>>> HandleDelete(
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
