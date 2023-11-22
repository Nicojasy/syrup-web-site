using Microsoft.EntityFrameworkCore;
using Syrup.Core.Db.Entities;
using Syrup.Core.Settings;
using Syrup.Infrastructure.Extensions;
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
services.AddDbContext<SyrupDbContext>(options =>
    options
        .UseNpgsql(dbConnectionString)
        .LogTo(Console.WriteLine));

services.AddInfrastructure(configuration);

var app = builder.Build();

await app.EnsureMigrationOfContextAsync<SyrupDbContext>();

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
