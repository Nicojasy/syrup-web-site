using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Result.Models;

namespace Syrup.Identity.Application.Interfaces.Services;
public interface IUserService
{
    Task<UserResponse> GetAsync(long id);
    Task<ValueOperationResult<UserResponse>> UpdateAsync(UpdateUserRequest request, long userId);
    Task<OperationResult> DeleteAsync(long id, long userId);
}
