using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syrup.Controller;
using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Identity.Application.Interfaces.Services;

namespace Syrup.Identity.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : CustomControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
        => _userService = userService;

    [HttpGet(nameof(Get))]
    public async Task<UserResponse> Get(long id)
      => await _userService.GetAsync(id);

    [Authorize]
    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
      => ServerResult(
          await _userService.UpdateAsync(request, GetUserId()));

    [Authorize]
    [HttpDelete(nameof(Delete))]
    public async Task<IActionResult> Delete(long id)
      => ServerResult(
          await _userService.DeleteAsync(id, GetUserId()));
}
