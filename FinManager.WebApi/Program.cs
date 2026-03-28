using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.WebApi.Configurations;
using FinManager.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;
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

app.Run();