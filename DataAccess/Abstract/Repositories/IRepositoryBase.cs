using Entities.Abstract;
using Entities.Abstract.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract.Repositories
{
    public interface IRepositoryBase<T> where T:BaseEntity,IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges = true);
        Task<T?> GetByIdAsync(int id, bool trackChanges = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool trackChanges = true);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
