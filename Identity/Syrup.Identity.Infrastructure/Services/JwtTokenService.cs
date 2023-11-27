using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Syrup.Authorize.Helpers;
using Syrup.Authorize.Options;
using Syrup.Identity.Application.Interfaces.Services;
using Syrup.Identity.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Syrup.Identity.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
        => _jwtOptions = jwtOptions;

    public AccessTokenWithExpirationModel GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtOptions.Value.SigningKey);
        var secretKey = new SymmetricSecurityKey(secretKeyBytes);
        var singingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var expirationDateTime = DateTime.UtcNow.AddSeconds(_jwtOptions.Value.ExpirationSeconds);
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: claims,
            expires: expirationDateTime,
            signingCredentials: singingCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new AccessTokenWithExpirationModel(tokenString, expirationDateTime);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = JwtBearerHelper.GetValidationParameters(_jwtOptions.Value);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is JwtSecurityToken jwtSecurityToken
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
        }
        catch
        {
            // Ignore exception and return null
        }
        
        return null;
    }
}
