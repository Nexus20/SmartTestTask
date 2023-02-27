using System.Linq.Expressions;
using SmartTestTask.Domain.Entities.Abstract;

namespace SmartTestTask.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetSingleByExpressionAsync(Expression<Func<TEntity, bool>> predicate);
}