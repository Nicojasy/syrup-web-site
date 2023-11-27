using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrup.Authorize.Helpers;
using Syrup.Authorize.Options;
using Syrup.Settings;

namespace Syrup.Authorize.Extensions;
public static class JwtBearerRegistrationExtensions
{
    public static IServiceCollection AddJwtBearerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = services.ConfigureAndGet<JwtOptions>(configuration);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
                opts.TokenValidationParameters = JwtBearerHelper.GetValidationParameters(jwtOptions));

        services.AddAuthorization();
        return services;
    }
}
