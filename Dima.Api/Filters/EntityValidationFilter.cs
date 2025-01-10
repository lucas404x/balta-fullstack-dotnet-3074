using Dima.Core.Entities;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Api.Filters;

internal class EntityValidationFilter<TEntity> : IEndpointFilter
    where TEntity : BaseEntity
{
    private static readonly Type _requestType = typeof(BaseRequestWithEntity<TEntity>);

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = (BaseRequestWithEntity<TEntity>?)context.Arguments.FirstOrDefault(x =>
        {
            var t = x?.GetType();
            return t is not null && t.IsSubclassOf(_requestType);
        });
        if (request is not null)
        {
            string? errorMsg = request.Entity.Validate().FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                var response = new ApiResponse<object> { ErrorMessage = errorMsg };
                return Results.UnprocessableEntity(response);
            }
        }
        return await next.Invoke(context);
    }
}