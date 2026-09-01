using FinManager.DataAccess.Configurations;
using FinManager.WebApi.ApiModels.Category;
using FinManager.WebApi.ApiModels.User;
using FinManager.WebApi.Handlers;
using FinManager.WebApi.Services;
using FinManager.WebApi.Validators;
using FinManager.WebApi.Validators.User;
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

        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<CategoryService>();

        builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();

        builder.Services.AddScoped<ValidationFilter<UserRequest>>();
        builder.Services.AddScoped<ValidationFilter<CategoryCreateRequest>>();
        builder.Services.AddScoped<ValidationFilter<CategoryUpdateRequest>>();

        builder.Services.AddControllers();

        return builder;
    }
}
