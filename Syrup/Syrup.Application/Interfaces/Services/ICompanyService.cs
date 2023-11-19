using Syrup.Application.Dtos.Requests;
using Syrup.Application.Dtos.Responses;
using Syrup.Core.Db.Entities;
using Syrup.Result.Models;

namespace Syrup.Application.Interfaces.Services;
public interface ICompanyService
{
    Task<CompanyResponse?> GetAsync(long companyId);
    Task<ValueOperationResult<Company>> CreateAsync(CreateCompanyRequest request, long creatorId);
    Task<ValueOperationResult<Company>> EditAsync(EditCompanyRequest request, long userId);
    Task<OperationResult> DeleteAsync(long companyId, long userId);
}
