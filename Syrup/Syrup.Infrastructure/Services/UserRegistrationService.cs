using Common.ResultLib.Helpers;
using Common.ResultLib.Models;
using IdGen;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Application.Models.Requests;
using Syrup.Core.Database.Entities;

namespace Syrup.Application.Interfaces.Services;
public class UserRegistrationService : IUserRegistrationService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IdGenerator _idGenerator;

    public CompanyRegistrationService(
        ICompanyRepository companyRepository,
        IdGenerator idGenerator)
    {
        _companyRepository = companyRepository;
        _idGenerator = idGenerator;
    }

    public async Task<ValueOperationResult<Company>> Register(CreateCompanyRequest request, long creatorId)
    {
        if (IsValidCompanyName(request.Name))
            return ResultHelper.Bad<Company>("Incorrect company name");

        var company = await _companyRepository.GetAsync(request.Name);
        if (company is not null)
            return ResultHelper.Bad<Company>("The company name is already in use");

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
        };
        await _companyRepository.AddCompanyUserAsync(newCompanyUser);

        return ResultHelper.Success(newCompany);
    }

    private static bool IsValidCompanyName(string companyName) =>
        string.IsNullOrWhiteSpace(companyName) || companyName.Length <= 2 || companyName.Length >= 256;
}