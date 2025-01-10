using Dima.Api.Endpoints;
using Dima.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiDocs();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnv();
}

app.UseSecurity();

app.MapEndpoints();

app.Run();