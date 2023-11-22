using Microsoft.EntityFrameworkCore;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Core.Db.Entities;

namespace Syrup.Infrastructure.Db.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly SyrupDbContext _syrupContext;

    public CompanyRepository(SyrupDbContext syrupContext) => _syrupContext = syrupContext;

    public ValueTask<Company?> GetAsync(long id) =>
        _syrupContext.Companies.FindAsync(id);

    public Task<Company?> GetAsync(string name) =>
        _syrupContext.Companies.FirstOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Company company)
    {
        await _syrupContext.Companies.AddAsync(company);
        await _syrupContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Company company)
    {
        _syrupContext.Companies.Update(company);
        return _syrupContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long companyId)
    {
        var t = await _syrupContext.Companies.FindAsync(companyId);
        if (t is not null)
        {
            t.IsDeleted = true;
            await _syrupContext.SaveChangesAsync();
        }
    }

    public Task<CompanyUser?> GetCompanyUserAsync(long companyId, long userId) =>
        _syrupContext.CompanyUsers.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.UserId == userId);

    public async Task AddCompanyUserAsync(CompanyUser companyUser)
    {
        await _syrupContext.CompanyUsers.AddAsync(companyUser);
        await _syrupContext.SaveChangesAsync();
    }

    public Task DeleteCompanyUserAsync(CompanyUser companyUser)
    {
        _syrupContext.CompanyUsers.Remove(companyUser);
        return _syrupContext.SaveChangesAsync();
    }
}
