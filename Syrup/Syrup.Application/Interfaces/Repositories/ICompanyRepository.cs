using Syrup.Core.Database.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    ValueTask<Company?> GetAsync(long id);
    Task<Company?> GetAsync(string name);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task DeleteAsync(long companyId);

    Task AddCompanyUserAsync(CompanyUser companyUser);
    Task DeleteCompanyUserAsync(CompanyUser companyUser);
}
