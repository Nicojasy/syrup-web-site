﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Syrup.Settings;
public static class SettingsExtensions
{
    public static TOptions ConfigureAndGet<TOptions>(this IServiceCollection services, IConfiguration configuration)
         where TOptions : class
    {
        var configureSections = configuration.GetRequiredSection(nameof(TOptions));
        services.Configure<TOptions>(configureSections);
        return configureSections.Get<TOptions>()
            ?? throw new ArgumentNullException();
    }
}