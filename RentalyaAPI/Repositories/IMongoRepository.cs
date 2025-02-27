using System.Linq.Expressions;
using RentalyaAPI.Models.Base;

namespace RentalyaAPI.Repositories
{
    public interface IMongoRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);
        Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> filterExpression);
        Task CreateAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
} 