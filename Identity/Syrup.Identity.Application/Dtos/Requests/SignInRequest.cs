namespace Syrup.Identity.Application.Dtos.Requests;

public record SignInRequest(
    string Login,
    string Password);
