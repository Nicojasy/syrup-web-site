namespace Syrup.Identity.Application.Dtos.Requests;

public record SignupRequest(
    string Login,
    string Password);
