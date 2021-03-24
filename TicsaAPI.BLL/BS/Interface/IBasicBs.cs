using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBasicBs<T> where T : BasicElement
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Update(int id, T entity);

        Task<T> Remove(int id);

        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entityList);
        Task RemoveRange(IEnumerable<T> entityList);
        Task UpdateRange(IEnumerable<T> entityList);
    }
}
