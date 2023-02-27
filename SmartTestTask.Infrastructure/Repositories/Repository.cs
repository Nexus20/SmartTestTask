using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Domain.Entities.Abstract;

namespace SmartTestTask.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext DbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual Task<TEntity?> GetSingleByExpressionAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return DbContext.Set<TEntity>()
            .AsNoTracking()
            .SingleOrDefaultAsync(predicate);
    }
}