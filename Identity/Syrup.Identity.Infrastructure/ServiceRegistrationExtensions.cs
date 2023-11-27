using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.Authorize.Extensions;
using Syrup.Identity.Application;
using Syrup.Identity.Application.Interfaces.Repositories;
using Syrup.Identity.Application.Interfaces.Services;
using Syrup.Identity.Core.Db;
using Syrup.Identity.Infrastructure.Db;
using Syrup.Identity.Infrastructure.Db.Repositories;
using Syrup.Identity.Infrastructure.Services;
using Syrup.IdGen.Extensions;

namespace Syrup.Identity.Infrastructure;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString(ConnectionConstants.SyrupIdentityConnection)
            ?? throw new ArgumentNullException(ConnectionConstants.SyrupIdentityConnection);
        
        return services
            .AddAutoMapper(ApplicationAssembly.Get())
            .AddIdGenServices(configuration)
            .AddDbContext(dbConnectionString)
            .AddIdentity(dbConnectionString)
            .AddServices()
            .AddRepositories()
            .AddJwtBearerAuth(configuration);
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<SyrupIdentityDbContext>(options =>
            options
                .UseNpgsql(connectionString)
                .LogTo(Console.WriteLine));
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, string connectionString)
    {
        var migrationsAssembly = typeof(ServiceRegistrationExtensions).Assembly.GetName().Name;
        services.AddIdentityServer()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(
                    connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(
                    connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            });
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IAuthorizeService, AuthorizeService>()
            .AddScoped<IJwtTokenService, JwtTokenService>()
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IUserRegistrationService, UserRegistrationService>()
            .AddScoped<IUserService, UserService>();

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IRefreshSessionRepository, RefreshSessionRepository>()
            .AddScoped<IUserRepository, UserRepository>();
}
