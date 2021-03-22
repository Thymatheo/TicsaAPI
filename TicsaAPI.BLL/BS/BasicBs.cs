using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public abstract class BasicBs<T> : IBasicBs<T> where T : BasicElement
    {
        public IBasicDp<T> Dp { get; set; }

        public BasicBs(IBasicDp<T> dp)
        {
            Dp = dp;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Dp.GetAll();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Dp.GetById(id);
        }

        public abstract Task<T> Update(int id, T entity);

        public virtual async Task<T> Remove(int id)
        {
            var entity = await Dp.GetById(id);
            return await Dp.Remove(entity);
        }

        public virtual async Task<T> Add(T entity)
        {
            return await Dp.Add(entity);
        }
    }
}
