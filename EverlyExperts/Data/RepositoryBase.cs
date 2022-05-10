using EverlyExperts.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EverlyExperts.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext RepositoryContext { get; set; }
        public RepositoryBase(ApplicationDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            RepositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    }
}
