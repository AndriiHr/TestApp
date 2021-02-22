using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Domain.IRepositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includePaths);

        Task Insert(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task Save();
    }
}