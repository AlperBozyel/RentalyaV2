using MongoDB.Driver;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using RentalyaAPI.Configurations;
using RentalyaAPI.Models.Base;
using RentalyaAPI.Attributes;

namespace RentalyaAPI.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            
            var collectionName = GetCollectionName(typeof(T));
            _collection = database.GetCollection<T>(collectionName);
        }

        private protected string GetCollectionName(Type documentType)
        {
            var attribute = documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute;

            return attribute?.CollectionName ?? documentType.Name.ToLowerInvariant();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            await _collection.DeleteOneAsync(filter);
        }
    }
} 