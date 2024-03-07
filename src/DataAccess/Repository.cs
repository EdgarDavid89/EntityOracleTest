using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class 
    {
        private readonly ModelContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ModelContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(TId Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TId Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }
    }  
}