using Syrup.Identity.Application.Dtos.Requests;
using Syrup.Result.Models;

namespace Syrup.Identity.Application.Interfaces.Services;
public interface IUserRegistrationService
{
    Task<OperationResult> SignupAsync(SignupRequest request);
}
