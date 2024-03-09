using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
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

        public async Task<TEntity?> GetByIdAsync<TId>(TId Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<TEntity> UpdateAsync<TId>(TEntity entity, TId id)
        {
            var entityExist = await _dbSet.FindAsync(id);
            if(entityExist == null)
            {
                throw new DbUpdateConcurrencyException("Entity not found");
            }
            _context.Entry(entityExist).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync<TId>(TId Id)
        {
            var entityExist = await _dbSet.FindAsync(Id);
            if(entityExist == null)
            {
                throw new DbUpdateConcurrencyException("Entity not found");
            }
            _dbSet.Remove(entityExist);
            await _context.SaveChangesAsync();
            return entityExist;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }
    }  
}