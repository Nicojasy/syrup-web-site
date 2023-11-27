using AutoMapper;
using IdGen;
using Syrup.Application.Dtos.Requests;
using Syrup.Application.Dtos.Responses;
using Syrup.Application.Interfaces.Repositories;
using Syrup.Application.Interfaces.Services;
using Syrup.Core.Db.Entities;
using Syrup.Core.Db.Enums;
using Syrup.Result.Helpers;
using Syrup.Result.Models;

namespace Syrup.Infrastructure.Services;
public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IdGenerator _idGenerator;

    public CompanyService(
        IMapper mapper,
        ICompanyRepository companyRepository,
        IdGenerator idGenerator)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _idGenerator = idGenerator;
    }

    public async Task<CompanyResponse?> GetAsync(long companyId) =>
        _mapper.Map<CompanyResponse>(
            await _companyRepository.GetAsync(companyId));

    public async Task<ValueOperationResult<Company>> CreateAsync(CreateCompanyRequest request, long creatorId)
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
            Type = CompanyUserRole.Admin
        };
        await _companyRepository.AddCompanyUserAsync(newCompanyUser);

        return ResultHelper.Success(newCompany);
    }

    public async Task<ValueOperationResult<Company>> EditAsync(EditCompanyRequest request, long userId)
    {
        if (IsValidCompanyName(request.Name))
            return ResultHelper.Bad<Company>("Incorrect company name");

        var company = await _companyRepository.GetAsync(request.Id);
        if (company is null)
            return ResultHelper.Bad<Company>("The company not found");

        var companyUser = company.CompanyUsers.FirstOrDefault(x => x.UserId == userId);
        if (companyUser is null)
            return ResultHelper.Bad<Company>("No permission to change this company");

        company.Name = request.Name;
        company.Description = request.Description;
        await _companyRepository.UpdateAsync(company);

        return ResultHelper.Success(company);
    }

    public async Task<OperationResult> DeleteAsync(long companyId, long userId)
    {
        var companyUser = await _companyRepository.GetCompanyUserAsync(companyId, userId);
        if (companyUser is null || companyUser.Type != CompanyUserRole.Admin)
        {
            return ResultHelper.Bad("No permission to delete this company");
        }

        await _companyRepository.DeleteAsync(companyId);
        return ResultHelper.Success();
    }

    private static bool IsValidCompanyName(string companyName) =>
        string.IsNullOrWhiteSpace(companyName) || companyName.Length <= 2 || companyName.Length >= 256;
}
