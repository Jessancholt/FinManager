using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinManager.DataAccess.Configurations;

public static class DataAccessConfigurationExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
    {
        services.AddDbContext<AppDbContext>(action);
        return services;
    }

}
