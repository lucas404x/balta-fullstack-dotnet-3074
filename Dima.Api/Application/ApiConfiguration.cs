namespace Dima.Api.Application;

public static class ApiConfiguration
{
    public const string CorsPolicyName = "DimaPolicy";
    
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
}
