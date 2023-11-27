using Syrup.Migration.Extensions;
using Syrup.Infrastructure;
using Syrup.Infrastructure.Db;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddInfrastructure(configuration);

var app = builder.Build();

await app.EnsureMigrationOfContextAsync<SyrupDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
