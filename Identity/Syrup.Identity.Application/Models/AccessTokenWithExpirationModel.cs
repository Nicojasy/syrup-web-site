namespace Syrup.Identity.Application.Models;
public record class AccessTokenWithExpirationModel(
    string AccessToken,
    DateTime ExpirationDateTime);
