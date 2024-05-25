using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NetCoreMicroservices.Commons.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<T> FindAll() => Context.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            Context.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => Context.Set<T>().Add(entity);
        public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellation = default(CancellationToken))
        {
            await Context.Set<T>().AddAsync(entity, cancellation);
            return entity;
        }

        public void Update(T entity) => Context.Set<T>().Update(entity);
        public void Delete(T entity) => Context.Set<T>().Remove(entity);

        public async Task<int> SaveChangeAsync(CancellationToken cancellation = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellation);
        }
    }
}
