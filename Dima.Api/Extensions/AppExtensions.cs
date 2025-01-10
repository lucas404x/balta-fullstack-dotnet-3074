namespace Dima.Api.Extensions;

internal static class AppExtensions
{
    public static void ConfigureDevEnv(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        // app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseHttpsRedirection();
        // app.UseAuthentication();
        // app.UseAuthorization();
    }
}
