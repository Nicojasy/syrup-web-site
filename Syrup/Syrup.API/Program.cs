using IdGen.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Syrup.Core.Database.Entities;
using Syrup.Core.Settings;
using Syrup.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var serviceOptions = services.ConfigureAndGet<ServiceOptions>(configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnectionString = configuration.GetConnectionString(ConnectionConstants.SyrupApiConnection);
services.AddDbContext<SyrupContext>(options =>
    options
        .UseNpgsql(dbConnectionString)
        .LogTo(Console.WriteLine));

services
    .AddIdGen(123);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
