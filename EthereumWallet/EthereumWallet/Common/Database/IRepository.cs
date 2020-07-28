using System.Collections.Generic;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Database
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<T> GetByObject(T item);
        Task<int> DeleteAsync(T item);
        Task<List<T>> GetAllAsync();
        Task<int> SaveAsync(T item);
    }
}