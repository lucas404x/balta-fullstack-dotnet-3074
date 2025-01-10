﻿using Dima.Api.Data;
using Dima.Api.Data.Repositories;
using Dima.Api.Handlers.EntityHandler;
using Dima.Core.Entities;
using Dima.Core.Handlers.EntityHandlers;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Extensions;

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

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}
