using Microsoft.EntityFrameworkCore;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ContractRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateContractAsync(Contract newContract)
    {
        await _dbContext.Contracts.AddAsync(newContract);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Contract>> GetContractsAsync()
    {
        return _dbContext.Contracts
            .Include(x => x.IndustrialPremise)
            .Include(x => x.TechnicalEquipmentType)
            .AsNoTracking()
            .ToListAsync();
    }
}