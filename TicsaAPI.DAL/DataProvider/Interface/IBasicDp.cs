using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider.Interface
{
    public interface IBasicDp<T> where T : BasicElement
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Remove(T entity);
        Task RemoveRange(IEnumerable<T> entity);
        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        Task<T> Update(T entity);
        Task UpdateRange(IEnumerable<T> entity);
    }
}
