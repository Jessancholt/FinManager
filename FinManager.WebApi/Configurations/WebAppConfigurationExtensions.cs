using FinManager.DataAccess.Configurations;
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

        builder.Services.AddControllers();
        builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();

        return builder;
    }
}
