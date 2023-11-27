using System.Text;
using Microsoft.IdentityModel.Tokens;
using Syrup.Authorize.Options;

namespace Syrup.Authorize.Helpers;
public static class JwtBearerHelper
{
    public static TokenValidationParameters GetValidationParameters(JwtOptions jwtOptions)
    {
        var signingKeyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);

        return new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
    }
}
