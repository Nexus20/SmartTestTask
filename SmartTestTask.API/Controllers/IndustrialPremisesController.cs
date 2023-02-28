using Microsoft.AspNetCore.Mvc;
using SmartTestTask.API.Authentication;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKeyAuthorize]
public class IndustrialPremisesController : ControllerBase
{
    private readonly IIndustrialPremiseService _industrialPremiseService;

    public IndustrialPremisesController(IIndustrialPremiseService industrialPremiseService)
    {
        _industrialPremiseService = industrialPremiseService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<IndustrialPremiseResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _industrialPremiseService.GetIndustrialPremisesAsync();
        return Ok(result);
    }
}