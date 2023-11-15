using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.Application.Repositories;
using Syrup.Application.Repositories.Interfaces;

namespace Messenger.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IChatRepository, ChatRepository>();
    }
}
