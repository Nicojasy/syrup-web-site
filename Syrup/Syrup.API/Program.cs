using IdGen.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Syrup.Core.Models.Options;
using Syrup.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var serviceOptionsSection = configuration.GetSection(nameof(ServiceOptions)).;
services.Configure<ServiceOptions>(serviceOptionsSection);
var serviceOptions = serviceOptionsSection.Get<ServiceOptions>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnectionString = configuration.GetConnectionString("SyrupConnection");
services.AddDbContext<SyrupContext>(options => options.UseNpgsql(dbConnectionString));

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
