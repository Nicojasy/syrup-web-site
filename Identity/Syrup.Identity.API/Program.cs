using Syrup.Identity.Infrastructure;
using Syrup.Identity.Infrastructure.Db;
using Syrup.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddInfrastructure(configuration);

var app = builder.Build();

await app.EnsureMigrationOfContextAsync<SyrupIdentityDbContext>();

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
