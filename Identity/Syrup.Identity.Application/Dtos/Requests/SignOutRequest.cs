namespace Syrup.Identity.Application.Dtos.Requests;

public record SignOutRequest(
    string RefreshToken);
