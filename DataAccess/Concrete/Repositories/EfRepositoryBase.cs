using DataAccess.Abstract.Repositories;
using Entities.Abstract;
using Entities.Abstract.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
    where TEntity : BaseEntity, IEntity
    where TContext:DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepositoryBase(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = true)
        {
           if(trackChanges==false)
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id, bool trackChanges = true)
        {
            if (trackChanges == false)
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
            }
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool trackChanges = true)
        {
            if (trackChanges == false)
            {
                return _dbSet.AsNoTracking().Where(method);
            }
            return _dbSet.Where(method);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
