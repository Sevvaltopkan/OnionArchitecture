using OnionVb02.Application.DTOInterfaces;
using OnionVb02.Domain.Interfaces;

namespace OnionVb02.Application.ManagerInterfaces
{
    public interface IManager<T,U> where T: class,IDto where U:class,IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetUpdateds();

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<string> SoftDeleteAsync(int id);
        Task<string> HardDeleteAsync(int id);
    }
}
