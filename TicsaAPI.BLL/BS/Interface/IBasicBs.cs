using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBasicBs<T> where T : BasicElement
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Update(T entity);

        Task<T> Remove(T entity);

        Task<T> Add(T entity);
    }
}
