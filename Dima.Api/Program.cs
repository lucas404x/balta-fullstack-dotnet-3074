using Dima.Api.Application.Endpoints;
using Dima.Api.Application.Extensions;

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