using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.Interfaces.Repositories;

public interface IContractRepository
{
    public Task CreateContractAsync(Contract newContract);
    public Task<List<Contract>> GetContractsAsync();
}