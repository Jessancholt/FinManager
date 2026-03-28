
using FinManager.DataAccess.Configurations;
using FinManager.WebApi.Handlers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FinManager.WebApi.Configurations
{
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

            return builder;
        }
    }
}
