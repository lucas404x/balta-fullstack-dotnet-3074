using Dima.Api.Domain.Abstractions;
using Dima.Api.Domain.Models;
using Dima.Core.Entities.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.Api.Application.Endpoints;

internal class IdentityEndpoint : IEndpointGroup
{
    public static RouteGroupBuilder Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("v1/identity")
            .WithSummary("Identity")
            .WithTags("Identity");

        group.MapIdentityApi<UserModel>();
        group.MapGet("/roles", HandleRoles).RequireAuthorization();
        group.MapPost("/logout", HandleLogout).RequireAuthorization();

        return group;
    }

    private static IResult HandleRoles(
        ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated) return TypedResults.Unauthorized();
        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(c => new RoleClaim
            {
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            })
            .ToList();
        return TypedResults.Ok(roles);
    }

    private async static Task<IResult> HandleLogout(
        SignInManager<UserModel> signInManager)
    {
        await signInManager.SignOutAsync();
        return TypedResults.Ok();
    }
}
