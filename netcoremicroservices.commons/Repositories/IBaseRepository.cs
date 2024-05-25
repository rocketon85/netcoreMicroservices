using System.Linq.Expressions;

namespace NetCoreMicroservices.Commons.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        Task<T> CreateAsync(T entity, CancellationToken cancellation = default(CancellationToken));
        void Update(T entity);
        void Delete(T entity);
    }
}
