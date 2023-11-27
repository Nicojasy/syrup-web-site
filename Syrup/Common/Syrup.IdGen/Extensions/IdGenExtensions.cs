using IdGen.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.IdGen.Settings;

namespace Syrup.IdGen.Extensions;

public static class IdGenExtensions
{
    public static IServiceCollection AddIdGenServices(this IServiceCollection services, IConfiguration configuration)
    {
        //todo: change exception type
        var idGenOptions = configuration.GetRequiredSection(nameof(IdGenOptions)).Get<IdGenOptions>()
            ?? throw new ArgumentException(nameof(IdGenOptions));
        services.AddIdGen(idGenOptions.GeneratorId);
        return services;
    }
}
