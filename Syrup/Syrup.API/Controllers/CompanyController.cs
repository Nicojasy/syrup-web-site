using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syrup.Application.Dtos.Requests;
using Syrup.Application.Dtos.Responses;
using Syrup.Application.Interfaces.Services;
using Syrup.Controller;

namespace Syrup.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class CompanyController : CustomControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet(nameof(Get))]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    public async Task<CompanyResponse?> Get([FromRoute] long id) =>
        await _companyService.GetAsync(id);

    /*
    [HttpGet(nameof(Get))]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    public async Task<CompanyResponse?> GetFiltered([FromRoute] long id) =>
        await _companyService.GetAsync(id);
    */

    [HttpPost(nameof(Create))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
    {
        var result = await _companyService.CreateAsync(request, GetUserId());
        return result.IsSuccess
            ? Created($"company/{result.Value}", null)
            : BadRequest();
    }

    [HttpPut(nameof(Edit))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Edit([FromBody] EditCompanyRequest request)
    {
        var result = await _companyService.EditAsync(request, GetUserId());
        return ServerResult(result);
    }

    [HttpDelete(nameof(Delete))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var result = await _companyService.DeleteAsync(id, GetUserId());
        return ServerResult(result);
    }
}
