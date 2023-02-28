using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.Repositories;

public class IndustrialPremiseRepository : Repository<IndustrialPremise>
{
    public IndustrialPremiseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override Task<IndustrialPremise?> GetSingleByExpressionAsync(Expression<Func<IndustrialPremise, bool>> predicate)
    {
        return DbContext.IndustrialPremises
            .Include(x => x.Contracts)
            .ThenInclude(x => x.TechnicalEquipmentType)
            .AsNoTracking()
            .SingleOrDefaultAsync(predicate);
    }
}