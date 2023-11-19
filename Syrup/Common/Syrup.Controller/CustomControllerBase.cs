using Microsoft.AspNetCore.Mvc;
using Syrup.Result.Helpers;
using Syrup.Result.Models;

namespace Syrup.Controller;

[ProducesResponseType(typeof(ErrorResponse), 500)]
public class CustomControllerBase : ControllerBase
{
    protected long GetUserId() => long.Parse(HttpContext.User.Identity!.Name!);

    protected IActionResult ServerResult(OperationResult result) =>
        result.IsSuccess ? Ok() : BadRequest(result.GetErrorResponse());

    protected IActionResult ServerResult<T>(ValueOperationResult<T> result) =>
        result.IsSuccess ? Ok(result.Value) : BadRequest(result.GetErrorResponse());
}
