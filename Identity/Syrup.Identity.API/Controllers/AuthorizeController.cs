using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syrup.Controller;
using Syrup.Identity.Application.Dtos.Requests;
using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Identity.Application.Interfaces.Services;

namespace Syrup.Identity.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AuthorizeController : CustomControllerBase
{
    private readonly IAuthorizeService _authorizeService;
    private readonly IUserRegistrationService _userRegistrationService;

    public AuthorizeController(
        IAuthorizeService authorizeService,
        IUserRegistrationService userRegistrationService)
    {
        _authorizeService = authorizeService;
        _userRegistrationService = userRegistrationService;
    }

    [AllowAnonymous]
    [HttpPost(nameof(Signup))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
      => ServerResult(
          await _userRegistrationService.SignupAsync(request));

    [AllowAnonymous]
    [HttpPost(nameof(SignIn))]
    [ProducesResponseType(typeof(AuthTokensResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var result = await _authorizeService.SignInAsync(request);
        return result.IsSuccess ? Ok(result.Value) : Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost(nameof(Refresh))]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await _authorizeService.RefreshAsync(request);
        return result.IsSuccess ? Ok(result.Value) : Unauthorized();
    }

    [HttpPost(nameof(SignOut))]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignOut([FromBody] SignOutRequest request)
      => ServerResult(
          await _authorizeService.SignOutAsync(request, GetUserId()));
}
