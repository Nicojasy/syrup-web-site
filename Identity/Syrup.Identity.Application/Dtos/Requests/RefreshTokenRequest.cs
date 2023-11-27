namespace Syrup.Identity.Application.Dtos.Requests;

public record RefreshTokenRequest(
    string AccessToken,
    string RefreshToken);
