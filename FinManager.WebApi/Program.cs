using FinManager.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString(Config.ConnectionString)));

var app = builder.Build();

app.Run();