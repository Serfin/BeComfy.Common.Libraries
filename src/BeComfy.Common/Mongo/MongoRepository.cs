using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BeComfy.Common.Mongo
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> 
        where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public MongoRepository(IMongoDatabase mongoDb, string collectionName)
        {
            _collection = mongoDb.GetCollection<TEntity>(collectionName);
        }

        public async Task AddAsync(TEntity entity)
            => await _collection.InsertOneAsync(entity);

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await _collection.Find(predicate).SingleOrDefaultAsync();

        public async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, int amount)
            => await _collection.AsQueryable()
                .Where(predicate)
                .Take(amount)
                .ToListAsync();

        public async Task<IEnumerable<TEntity>> BrowseAsync(int pageSize, int page)
            => await _collection.AsQueryable()
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<IEnumerable<TEntity>> BrowseAsync(int pageSize, int page, Expression<Func<TEntity, bool>> predicate)
            => await _collection.AsQueryable()
                .OrderBy(x => x.Id)
                .Where(predicate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task UpdateAsync(TEntity entity)
            => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(x => x.Id == id);
    }
}