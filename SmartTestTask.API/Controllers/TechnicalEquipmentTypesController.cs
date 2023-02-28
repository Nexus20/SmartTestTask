using Microsoft.AspNetCore.Mvc;
using SmartTestTask.API.Authentication;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKeyAuthorize]
public class TechnicalEquipmentTypesController : ControllerBase
{
    private readonly ITechnicalEquipmentTypeService _technicalEquipmentTypeService;

    public TechnicalEquipmentTypesController(ITechnicalEquipmentTypeService technicalEquipmentTypeService)
    {
        _technicalEquipmentTypeService = technicalEquipmentTypeService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TechnicalEquipmentTypeResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _technicalEquipmentTypeService.GetTechnicalEquipmentTypesAsync();
        return Ok(result);
    }
}