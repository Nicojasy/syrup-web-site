namespace Syrup.Application.Dtos.Requests;

public record EditCompanyRequest(
    long Id,
    string Name,
    string? Description);
