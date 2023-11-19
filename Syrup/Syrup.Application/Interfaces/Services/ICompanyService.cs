using Common.ResultLib.Models;
using Syrup.Application.Models.Requests;
using Syrup.Core.Database.Entities;

namespace Syrup.Application.Interfaces.Services;
public interface ICompanyService
{
    Task<ValueOperationResult<Company>> Register(CreateCompanyRequest request, long creatorId);
}
