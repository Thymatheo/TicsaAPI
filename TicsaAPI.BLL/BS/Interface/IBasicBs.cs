using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBasicBs<T> where T : BasicElement
    {
        Task<IEnumerable<U>> GetAll<U>() where U : BasicElement;
        Task<U> GetById<U>(int id) where U : BasicElement;
        Task<U> Update<U, V>(int id, V entity) where U : BasicElement where V : BasicElement;
        Task<U> Remove<U>(int id) where U : BasicElement;
        Task<U> Add<U, V>(V entity) where U : BasicElement where V : BasicElement;
        Task AddRange<U>(IEnumerable<U> entityList) where U : BasicElement;
        Task RemoveRange<U>(IEnumerable<U> entityList) where U : BasicElement;
        Task UpdateRange<U>(IEnumerable<U> entityList) where U : BasicElement;
    }
}
