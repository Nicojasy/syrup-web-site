using System.Security.Claims;
using Syrup.Identity.Application.Models;

namespace Syrup.Identity.Application.Interfaces.Services;

public interface IJwtTokenService
{
    AccessTokenWithExpirationModel GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
