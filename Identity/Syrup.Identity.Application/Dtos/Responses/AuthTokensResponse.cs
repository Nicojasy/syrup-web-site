namespace Syrup.Identity.Application.Dtos.Responses;

public record AuthTokensResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpirationDateTime);
