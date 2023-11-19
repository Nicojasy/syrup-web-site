namespace Syrup.Application.Dtos.Requests;

public record CreateCompanyRequest(
    string Name,
    string? Description);
