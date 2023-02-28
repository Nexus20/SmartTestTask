using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.Interfaces.Repositories;

public interface IContractRepository : IRepository<Contract>
{
    public Task CreateContractAsync(Contract newContract);
}