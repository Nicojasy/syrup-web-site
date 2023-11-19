namespace Syrup.Application.Models.Requests;

public record CreateCompanyRequest(
    string Name,
    string? Description);
