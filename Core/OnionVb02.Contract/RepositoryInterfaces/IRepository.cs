using OnionVb02.Domain.Interfaces;
using System.Linq.Expressions;

namespace OnionVb02.Contract.RepositoryInterfaces
{
    public interface IRepository<T> where T:class,IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> exp);

        Task CreateAsync(T entity);
        Task UpdateAsync(T oldEntity,T newEntity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
