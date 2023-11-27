using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.Application;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Application.Interfaces.Services;
using Syrup.Core.Db;
using Syrup.IdGen.Extensions;
using Syrup.Infrastructure.Db;
using Syrup.Infrastructure.Db.Repositories;
using Syrup.Infrastructure.Services;

namespace Syrup.Infrastructure;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAutoMapper(ApplicationAssembly.Get())
            .AddIdGenServices(configuration)
            .AddServices()
            .AddRepositories()
            .AddDbContext(configuration)
            .AddJwtBearerAuth(configuration);
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString(ConnectionConstants.SyrupApiConnection);
        return services.AddDbContext<SyrupDbContext>(options =>
            options
                .UseNpgsql(dbConnectionString)
                .LogTo(Console.WriteLine));
    }

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<ICompanyService, CompanyService>();

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IChatRepository, ChatRepository>()
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IUserRepository, UserRepository>();
}
