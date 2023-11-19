using IdGen.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Application.Repositories;
using Syrup.Core.Settings;

namespace Messenger.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddIdGenServices(configuration)
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

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IChatRepository, ChatRepository>()
            .AddScoped<ICompanyRepository, CompanyRepository>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IUserRepository, UserRepository>();
}
