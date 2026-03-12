
using FinManager.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Configurations
{
    public static class WebAppConfigurationExtensions
    {
        public static WebApplicationBuilder ConfigureWebApp(this WebApplicationBuilder builder)
        {
            builder.Services.AddDataAccess(opt => opt.UseSqlServer(
                builder.Configuration.GetConnectionString(Config.DefaultConnection)));

            builder.Services.AddOpenApi();

            return builder;
        }
    }
}
