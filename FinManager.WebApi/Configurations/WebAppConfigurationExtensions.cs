using FinManager.DataAccess.Configurations;
using FinManager.WebApi.Endpoints;
using FinManager.WebApi.Handlers;
using FinManager.WebApi.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FinManager.WebApi.Configurations;

public static class WebAppConfigurationExtensions
{
    public static WebApplicationBuilder ConfigureWebApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddDataAccess(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString(Config.DefaultConnection));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Services.AddOpenApi();

        builder.Services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services));

        builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
        builder.Services.AddEndpoints();

        return builder;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoints(app);
        }

        return app;
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddSingleton<IEndpoint, UserEndpoints>();
        services.AddSingleton<IEndpoint, CategoryEndpoints>();
        return services;
    }
}
