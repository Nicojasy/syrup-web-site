namespace Syrup.Application.Models.Requests;

public record CreateUserRequest(
    string Name,
    string? Description);
