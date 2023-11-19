using Syrup.Core.Db.Entities;

namespace Syrup.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    ValueTask<Company?> GetAsync(long id);
    Task<Company?> GetAsync(string name);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task DeleteAsync(long companyId);

    Task<CompanyUser?> GetCompanyUserAsync(long companyId, long userId);
    Task AddCompanyUserAsync(CompanyUser companyUser);
    Task DeleteCompanyUserAsync(CompanyUser companyUser);
}
