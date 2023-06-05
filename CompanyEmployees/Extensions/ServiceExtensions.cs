﻿using System.Reflection;
using Contracts;
using FluentMigrator.Runner;
using LoggerService;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => { });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureFluentMigrator(this IServiceCollection services, IConfiguration configuration) =>
        services.AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c =>
                c.AddSqlServer2016().WithGlobalConnectionString(configuration
                        .GetConnectionString("sqlConnection"))
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For.Migrations());
}