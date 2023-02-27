using SmartTestTask.Application.Models.Requests;
using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.Application.Interfaces.Services;

public interface IContractService
{
    public Task<ContractResult> CreateContractAsync(CreateContractRequest request);
    public Task<List<ContractResult>> GetContractsAsync();
}