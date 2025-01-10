namespace Dima.Api.Application.Extensions;

internal static class AppExtensions
{
    public static void ConfigureDevEnv(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        // app.MapSwagger().RequireAuthorization();
    }

    public static void UseApiMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseHttpsRedirection();
        // app.UseAuthentication();
        // app.UseAuthorization();
    }
}
