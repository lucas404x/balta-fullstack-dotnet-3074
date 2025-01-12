using Dima.Core.Entities;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Api.Application.Filters;

/// <summary>
/// Retrieves the parameter whose implements the <seealso cref="IRequestValidate"/> type and calls the validate method inside of it.
/// </summary>
internal class RequestValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = (IRequestValidate?)context.Arguments.FirstOrDefault(x => x?.GetType()?.IsAssignableTo(typeof(IRequestValidate)) ?? false);
        if (request is not null)
        {
            string? errorMsg = request.Validate();
            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                return TypedResults.BadRequest(new ApiResponse<object>(errorMsg, System.Net.HttpStatusCode.BadRequest));
            }
        }
        return await next.Invoke(context);
    }
}