using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace netcoreAPI.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
