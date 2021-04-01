using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;
using TicsaAPI.BLL.DTO.Order;
using System;

namespace TicsaAPI.BLL.BS
{
    public class BsOrder : BasicBs<Order>, IBsOrder
    {
        public IDpOrder DpOrder { get; set; }
        public BsOrder(IDpOrder dp) : base(dp)
        {
            DpOrder = dp;
        }
        public async Task<IEnumerable<DtoOrder>> GetByIdClient(int idClient)
        {
            Mapper mapper = BuildMapper<Order, DtoOrder>();
            List<DtoOrder> result = new List<DtoOrder>();
            foreach (Order entity in await DpOrder.GetByIdClient(idClient))
                result.Add(mapper.Map<DtoOrder>(entity));
            return result;
        }
        public override async Task<U> Update<U, V>(int id, V entity) where U : class where V : class
        {
            var sourceEntity = await DpOrder.GetById(id);
            var updateEntity = BuildMapper<V, DtoOrderUpdate>().Map<DtoOrderUpdate>(entity);
            if (VerifyEntityUpdate(updateEntity.IdClient, sourceEntity.IdClient))
                sourceEntity.IdClient = (int)updateEntity.IdClient;
            if (VerifyEntityUpdate(updateEntity.OrderDate, sourceEntity.OrderDate))
                sourceEntity.OrderDate = (DateTime)updateEntity.OrderDate;
            return await base.Update<U, Order>(id, sourceEntity);
        }
    }
}
