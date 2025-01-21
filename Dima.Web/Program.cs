using Dima.Core.Handlers;
using Dima.Web;
using Dima.Web.Auth;
using Dima.Web.Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

AppConfiguration.BackendUrl = builder.Configuration["BackendUrl"] ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x =>
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpClient(AppConfiguration.HttpClientName, client =>
{
    client.BaseAddress = new Uri($"{AppConfiguration.BackendUrl}/v1/");
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddScoped<IAccountHandler, AccountHandler>();


await builder.Build().RunAsync();
