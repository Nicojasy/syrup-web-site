namespace Syrup.Identity.Application.Dtos.Responses;

public record UserResponse(
    long Id,
    string Nickname,
    string? Email,
    string? AboutMyself,
    DateTime RegistrationDateTime,
    bool IsDeleted);
