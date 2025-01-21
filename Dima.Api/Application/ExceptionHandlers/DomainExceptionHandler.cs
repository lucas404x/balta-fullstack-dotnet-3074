using Dima.Core.Exceptions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace Dima.Api.Application.ExceptionHandlers;

internal class DomainExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DomainException domainException) return false;
        var response = new ApiResponse<object>(domainException.Message, domainException.Code);
        httpContext.Response.StatusCode = (int)domainException.Code;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}
