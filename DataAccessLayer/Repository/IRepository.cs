using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllAsQuery();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAsQuery(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
