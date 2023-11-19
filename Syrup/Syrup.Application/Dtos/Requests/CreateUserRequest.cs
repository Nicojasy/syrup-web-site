namespace Syrup.Application.Dtos.Requests;

public record CreateUserRequest(
    string Name,
    string? Description);
