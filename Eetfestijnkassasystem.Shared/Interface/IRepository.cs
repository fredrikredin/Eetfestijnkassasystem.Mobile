using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eetfestijnkassasystem.Shared.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
