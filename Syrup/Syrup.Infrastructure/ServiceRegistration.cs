using IdGen.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.Application;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Application.Interfaces.Services;
using Syrup.Core.Settings;
using Syrup.Infrastructure.Repositories;
using Syrup.Infrastructure.Services;

namespace Syrup.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAutoMapper(ApplicationAssembly.Get())
            .AddIdGenServices(configuration)
            .AddServices()
            .AddRepositories();
    }

    public static IServiceCollection AddIdGenServices(this IServiceCollection services, IConfiguration configuration)
    {
        //todo: change exception type
        var idGenOptions = configuration.GetRequiredSection(nameof(IdGenOptions)).Get<IdGenOptions>()
            ?? throw new ArgumentException(nameof(IdGenOptions));
        services.AddIdGen(idGenOptions.GeneratorId);
        return services;
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
