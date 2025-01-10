using Dima.Core.Responses;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Dima.Api.Application.ExceptionHandlers;

internal class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<object>("An error occurred", HttpStatusCode.InternalServerError);
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}
