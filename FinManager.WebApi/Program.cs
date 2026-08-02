using FinManager.WebApi.Configurations;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureWebApp();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();