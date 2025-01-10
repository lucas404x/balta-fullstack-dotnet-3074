using Dima.Core.Entities;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Api.Application.Filters;

/// <summary>
/// Retrieves the parameter whose inherithes from <seealso cref="BaseRequestWithEntity{T}"/> and validates the entity.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal class EntityValidationFilter<TEntity> : IEndpointFilter
    where TEntity : BaseEntity
{
    private static readonly Type _requestType = typeof(BaseRequestWithEntity<TEntity>);

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = (BaseRequestWithEntity<TEntity>?)context.Arguments.FirstOrDefault(x => x?.GetType()?.IsSubclassOf(_requestType) ?? false);
        if (request is not null)
        {
            string? errorMsg = request.Entity.Validate().FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                return Results.UnprocessableEntity(new ApiResponse<TEntity>(errorMsg, System.Net.HttpStatusCode.UnprocessableEntity));
            }
        }
        return await next.Invoke(context);
    }
}