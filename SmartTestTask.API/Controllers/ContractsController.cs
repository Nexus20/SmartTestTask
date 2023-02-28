using Microsoft.AspNetCore.Mvc;
using SmartTestTask.API.Authentication;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Requests;
using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKeyAuthorize]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ContractResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _contractService.GetContractsAsync();
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ContractResult), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CreateContractRequest request)
    {
        var result = await _contractService.CreateContractAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}