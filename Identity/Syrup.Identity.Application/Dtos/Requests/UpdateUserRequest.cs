namespace Syrup.Identity.Application.Dtos.Responses;

public record UpdateUserRequest(
    long Id,
    string? Email,
    string? AboutMyself);
