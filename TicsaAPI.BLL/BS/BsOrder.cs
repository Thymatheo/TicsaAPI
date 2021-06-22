using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;
using TicsaAPI.BLL.DTO.Order;
using System;
using TicsaAPI.BLL.Extension;

namespace TicsaAPI.BLL.BS
{
    public class BsOrder
    {
        public IDpOrder DpOrder { get; set; }
        public IBsOrderContent BsOrderContent { get; set; }
        public BsOrder(IDpOrder dp, IBsOrderContent bsOrderContent)
        {
            DpOrder = dp;
        }
        public async Task<IEnumerable<DtoOrder>> GetAll() {
            List<DtoOrder> result = new List<DtoOrder>();
            (await DpOrder.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoOrder> GetById(int id) =>
            (await DpOrder.GetById(id)).ToDto();

        public async Task<DtoOrder> Update(int id, DtoOrderUpdate entity) =>
            (await DpOrder.Update(UpdateData(await DpOrder.GetById(id), entity))).ToDto();

        private Order UpdateData(Order target, DtoOrderUpdate source) {
            if (source.OrderDate != null)
                if (source.OrderDate != target.OrderDate)
                    target.OrderDate = (DateTime)source.OrderDate;
            if (source.IdClient != null)
                if (source.IdClient != target.IdClient)
                    target.IdClient = (int)source.IdClient;
            return target;
        }

        public async Task<DtoOrder> Remove(int id) =>
            (await DpOrder.Remove(await DpOrder.GetById(id))).ToDto();


        public async Task<DtoOrderAdd> Add(Order entity) =>
            (await DpOrder.Add(entity)).ToDtoAdd();

        public async Task AddRange(IEnumerable<Order> entityList) =>
            await DpOrder.AddRange(entityList);

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<Order> entityToRemove = (await DpOrder.GetAll()).ToList();
            await DpOrder.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }
        public async Task UpdateRange(Dictionary<int, DtoOrderUpdate> entityList) {
            List<Order> entityToUpdate = new List<Order>();
            IEnumerable<Order> entities = await DpOrder.GetAll();
            foreach (KeyValuePair<int, DtoOrderUpdate> entity in entityList)
                entityToUpdate.Add(UpdateData(entities.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            await DpOrder.UpdateRange(entityToUpdate);
        }
        public async Task<IEnumerable<DtoOrder>> GetByIdClient(int idClient) {
            List<DtoOrder> result = new List<DtoOrder>();
            (await DpOrder.GetByIdClient(idClient)).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }
    }
}
