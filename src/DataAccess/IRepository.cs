using System.Linq.Expressions;

namespace DataAccess
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity?> GetByIdAsync(TId Id);

        Task<TEntity> UpdateAsync(TEntity entity, TId id);

        Task<TEntity> DeleteAsync(TId Id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter);

    }
}