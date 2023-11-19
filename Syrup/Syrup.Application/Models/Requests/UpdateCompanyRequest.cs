namespace Syrup.Application.Models.Requests;

public record UpdateCompanyRequest(
    long id,
    string Name,
    string? Description);
