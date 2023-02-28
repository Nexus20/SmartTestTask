using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.Repositories;

public class ContractRepository : Repository<Contract>, IContractRepository
{
    public ContractRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateContractAsync(Contract newContract)
    {
        await DbContext.Contracts.AddAsync(newContract);
        await DbContext.SaveChangesAsync();
    }

    public override Task<List<Contract>> GetAsync()
    {
        return DbContext.Contracts
            .Include(x => x.IndustrialPremise)
            .Include(x => x.TechnicalEquipmentType)
            .AsNoTracking()
            .ToListAsync();
    }

    public override Task<Contract?> GetSingleByExpressionAsync(Expression<Func<Contract, bool>> predicate)
    {
        return DbContext.Contracts
            .Include(x => x.IndustrialPremise)
            .Include(x => x.TechnicalEquipmentType)
            .AsNoTracking()
            .SingleOrDefaultAsync(predicate);
    }
}