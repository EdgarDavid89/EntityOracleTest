using System.Linq.Expressions;

namespace DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity?> GetByIdAsync<TId>(TId Id);

        Task<TEntity> UpdateAsync<TId>(TEntity entity, TId id);

        Task<TEntity> DeleteAsync<TId>(TId Id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter);

    }
}