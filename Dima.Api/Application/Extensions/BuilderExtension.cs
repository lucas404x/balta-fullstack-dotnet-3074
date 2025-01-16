using Dima.Api.Application.ExceptionHandlers;
using Dima.Api.Application.Handlers.EntityHandler;
using Dima.Api.Data;
using Dima.Api.Data.Repositories;
using Dima.Api.Domain.Abstractions;
using Dima.Api.Domain.Models;
using Dima.Core;
using Dima.Core.Handlers.EntityHandlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Application.Extensions;

internal static class BuilderExtension
{
    public static void AddApiDocs(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            // shows the full qualified name instead of class's name only.
            x.CustomSchemaIds(n => n.FullName);
        });
    }

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        var urlsSection = builder.Configuration.GetRequiredSection("Urls");
        Configuration.BackendUrl = urlsSection.GetValue<string>("Backend") ?? throw new InvalidDataException("Backend url must be provided.");
        Configuration.FrontendUrl = urlsSection.GetValue<string>("Frontend") ?? throw new InvalidDataException("Frontend url must be provided.");
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl]));
        });

        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        builder.Services
            .AddIdentityCore<UserModel>()
            .AddRoles<IdentityRoleModel>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        builder.Services.AddAuthorization();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Repositories
        builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

        // Handlers
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

        // Exception Handlers
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<DomainExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
