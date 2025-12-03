using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Contract.RepositoryInterfaces
{
    public interface IRepository<T> where T:BaseEntity
    {

         

        //Queries
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        List<T> Where(Expression<Func<T, bool>> exp);

        //db.Products.Where(x => x.UnitPrice < 20)

        // 1 . Kısım Argüman kısmı x 

        // 2. Kısım

        //Where(new Araba())

        //Commands
    }
}
