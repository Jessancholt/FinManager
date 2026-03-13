
using FinManager.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FinManager.WebApi.Configurations
{
    public static class WebAppConfigurationExtensions
    {
        public static WebApplicationBuilder ConfigureWebApp(this WebApplicationBuilder builder)
        {
            builder.Services.AddDataAccess(opt => opt.UseSqlServer(
                builder.Configuration.GetConnectionString(Config.DefaultConnection)));

            builder.Services.AddOpenApi();

            builder.Services.AddSerilog((services, lc) => lc
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services));

            return builder;
        }
    }
}
