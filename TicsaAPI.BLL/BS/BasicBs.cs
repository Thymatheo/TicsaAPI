using AutoMapper;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public abstract class BasicBs<T> : IBasicBs<T> where T : BasicElement
    {
        private IBasicDp<T> Dp { get; set; }

        public BasicBs(IBasicDp<T> dp)
        {
            Dp = dp;
        }

        public virtual async Task<IEnumerable<U>> GetAll<U>() where U : BasicElement
        {
            Mapper mapper = BuildMapper<T, U>();
            List<U> result = new List<U>();
            foreach (T entity in await Dp.GetAll())
                result.Add(mapper.Map<U>(entity));
            return result;
        }

        public virtual async Task<U> GetById<U>(int id) where U : BasicElement
        {
            return BuildMapper<T, U>().Map<U>(await Dp.GetById(id));
        }

        public virtual async Task<U> Update<U, V>(int id, V entity) where U : BasicElement where V : BasicElement
        {
            return BuildMapper<T, U>().Map<U>(await Dp.Update(BuildMapper<V, T>().Map(entity, await Dp.GetById(id))));
        }

        public virtual async Task<U> Remove<U>(int id) where U : BasicElement
        {
            return BuildMapper<T, U>().Map<U>(await Dp.Remove(await Dp.GetById(id)));
        }

        public virtual async Task<U> Add<U, V>(V entity) where U : BasicElement where V : BasicElement
        {
            return BuildMapper<T, U>().Map<U>(await Dp.Add(BuildMapper<V, T>().Map<T>(entity)));
        }

        public virtual async Task AddRange<U>(IEnumerable<U> entityList) where U : BasicElement
        {
            Mapper mapper = BuildMapper<U, T>();
            List<T> entityToAdd = new List<T>();
            foreach (U entity in entityList)
                entityToAdd.Add(mapper.Map<T>(entity));
            await Dp.AddRange(entityToAdd);
        }
        public virtual async Task RemoveRange<U>(IEnumerable<U> entityList) where U : BasicElement
        {
            Mapper mapper = BuildMapper<U, T>();
            List<T> entityToRemove = new List<T>();
            foreach (U entity in entityList)
                entityToRemove.Add(mapper.Map<T>(entity));
            await Dp.RemoveRange(entityToRemove);
        }
        public virtual async Task UpdateRange<U>(IEnumerable<U> entityList) where U : BasicElement
        {

            Mapper mapper = BuildMapper<U, T>();
            List<T> entityToUpdate = new List<T>();
            foreach (U entity in entityList)
                entityToUpdate.Add(mapper.Map<T>(entity));
            await Dp.RemoveRange(entityToUpdate);
        }

        public static Mapper BuildMapper<U, V>() where U : class where V : BasicElement
        {
            return new Mapper(new MapperConfiguration(config => config.CreateMap<U, V>()));
        }

        public static bool VerifyEntityUpdate(object entitySource, object entityDest)
        {
            if (entitySource != null && entityDest != null)
                if (entitySource.ToString() != entityDest.ToString())
                    return true;
            return false;
        }
    }
}
