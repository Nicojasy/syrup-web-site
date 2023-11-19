using Common.ResultLib.Helpers;
using Common.ResultLib.Models;
using IdGen;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Application.Models.Requests;
using Syrup.Core.Database.Entities;
using Syrup.Core.Database.Enums;

namespace Syrup.Application.Interfaces.Services;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IdGenerator _idGenerator;

    public CompanyService(
        ICompanyRepository companyRepository,
        IdGenerator idGenerator)
    {
        _companyRepository = companyRepository;
        _idGenerator = idGenerator;
    }

    public async Task<ValueOperationResult<Company>> Register(CreateCompanyRequest request, long creatorId)
    {
        if (IsValidCompanyName(request.Name)) return ResultHelper.Bad<Company>("Incorrect company name");

        var company = await _companyRepository.GetAsync(request.Name);
        if (company is not null) return ResultHelper.Bad<Company>("The company name is already in use");

        var newCompany = new Company
        {
            Id = _idGenerator.CreateId(),
            Name = request.Name,
            Description = request.Description,

        };
        await _companyRepository.AddAsync(newCompany);

        var newCompanyUser = new CompanyUser
        {
            Id = _idGenerator.CreateId(),
            CompanyId = newCompany.Id,
            UserId = creatorId,
            Type = CompanyUserRole.Admin
        };
        await _companyRepository.AddCompanyUserAsync(newCompanyUser);

        return ResultHelper.Success(newCompany);
    }
    
    public async Task<ValueOperationResult<Company>> Get(CreateCompanyRequest request, long creatorId)
    {

    }

    public async Task<ValueOperationResult<Company>> Update(UpdateCompanyRequest request, long userId)
    {
        if (IsValidCompanyName(request.Name)) return ResultHelper.Bad<Company>("Incorrect company name");

        var company = await _companyRepository.GetAsync(request.id);
        if (company is null) return ResultHelper.Bad<Company>("The company not found");

        var companyUser = company.CompanyUsers.FirstOrDefault(x=>x.UserId == userId);
        if (companyUser is null) return ResultHelper.Bad<Company>("No permission to change it");

        company.Name = request.Name;
        company.Description = request.Description;
        await _companyRepository.UpdateAsync(company);

        return ResultHelper.Success(company);
    }

    private static bool IsValidCompanyName(string companyName) =>
        string.IsNullOrWhiteSpace(companyName) || companyName.Length <= 2 || companyName.Length >= 256;
}
