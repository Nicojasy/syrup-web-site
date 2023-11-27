using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Syrup.Migration.Extensions;
public static class EnsureMigrationAppExtensions
{
    public static Task EnsureMigrationOfContextAsync<T>(this IApplicationBuilder app)
        where T : DbContext
    {
        var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var serviceScope = serviceScopeFactory.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<T>();
        return context.Database.MigrateAsync();
    }
}
