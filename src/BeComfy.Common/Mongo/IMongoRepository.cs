using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeComfy.Common.Mongo
{
    public interface IMongoRepository<TEntity> 
        where TEntity : IEntity
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> BrowseAsync(int pageSize, int page);
        Task<IEnumerable<TEntity>> BrowseAsync(int pageSize, int page, Expression<Func<TEntity, bool>> predicate);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id); 
    }
}