using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using netcoreAPI.Context;
using System.ComponentModel;
using System.Linq.Expressions;

namespace netcoreAPI.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext RepositoryContext { get; set; }
        public RepositoryBase(AppDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellation = default(CancellationToken))
        {
            await RepositoryContext.Set<T>().AddAsync(entity, cancellation);
            return entity;
        }

        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public async Task<int> SaveChangeAsync(CancellationToken cancellation = default(CancellationToken))
        {
            return await RepositoryContext.SaveChangesAsync(cancellation);
        }
    }
}
