namespace Syrup.Application.Dtos.Requests;

public record EditCompanyRequest(
    long id,
    string Name,
    string? Description);
