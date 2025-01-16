using Dima.Api.Domain.Abstractions;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Dima.Core.Requests.Transaction;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Dima.Api.Application.Endpoints.EntityEndpoints;

internal class TransactionEndpoint : IEndpointGroup
{
    public static RouteGroupBuilder Map(IEndpointRouteBuilder app)
    {
        var mapGroup = BaseEntityEndpoint<Transaction, ITransactionHandler>.Map(app);
        mapGroup
            .MapGet("/", HandleGetByPeriod)
            .RequireAuthorization()
            .WithDescription("Get all transactions whose CreatedDate is between the StartDate and EndDate range.");

        return mapGroup;
    }

    private static async Task<IValueHttpResult<PagedApiResponse<List<Transaction>>>> HandleGetByPeriod(
        [FromQuery] DateTime? StartDate, 
        [FromQuery] DateTime? EndDate, 
        [FromQuery] int PageNumber, 
        [FromQuery] int PageSize,
        ClaimsPrincipal user,
        ITransactionHandler transactionHandler,
        CancellationToken cancellationToken)
    {
        var request = new GetTransactionsByPeriodRequest
        {
            StartDate = StartDate,
            EndDate = EndDate,
            PageNumber = PageNumber,
            PageSize = PageSize,
            UserId = user.Identity!.Name!
        };
        string? errorMsg = request.Validate();
        if (!string.IsNullOrWhiteSpace(errorMsg))
        {
            var response = new PagedApiResponse<List<Transaction>>(errorMsg, HttpStatusCode.BadRequest);
            return TypedResults.BadRequest(response);
        }
        return TypedResults.Ok(await transactionHandler.GetByPeriod(request, cancellationToken));
    }
}
