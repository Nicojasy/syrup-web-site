using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Syrup.Settings;
public static class SettingsExtensions
{
    public static TOptions? ConfigureAndGet<TOptions>(this IServiceCollection services, IConfiguration configuration)
         where TOptions : class
    {
        var typeName = typeof(TOptions).Name ?? string.Empty;
        var configureSections = configuration.GetSection(typeName);
        services.Configure<TOptions>(configureSections);
        return configureSections.Get<TOptions>();
    }
}
