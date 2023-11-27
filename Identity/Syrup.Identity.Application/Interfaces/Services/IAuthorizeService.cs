using Syrup.Identity.Application.Dtos.Requests;
using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Result.Models;

namespace Syrup.Identity.Application.Interfaces.Services;
public interface IAuthorizeService
{
    Task<ValueOperationResult<AuthTokensResponse>> SignInAsync(SignInRequest request);
    Task<ValueOperationResult<AuthTokensResponse>> RefreshAsync(RefreshTokenRequest request);
    Task<OperationResult> SignOutAsync(SignOutRequest request, long userId);
}
