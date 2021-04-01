using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.BLL.DTO.Order;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.DAL.DataProvider;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrderContent : BasicBs<OrderContent>, IBsOrderContent
    {
        private IDpOrderContent DpOrderContent { get; set; }
        private IBsGamme BsGamme { get; set; }

        public BsOrderContent(IDpOrderContent dp, IBsGamme bsGamme) : base(dp)
        {
            DpOrderContent = dp;
            BsGamme = bsGamme;
        }
        public async Task<IEnumerable<DtoOrderContent>> GetByIdOrder(int idOrder)
        {
            Mapper mapper = BuildMapper<OrderContent, DtoOrderContent>();
            List<DtoOrderContent> result = new List<DtoOrderContent>();
            foreach (OrderContent entity in await DpOrderContent.GetByIdOrder(idOrder))
                result.Add(mapper.Map<DtoOrderContent>(entity));
            return result;
        }

        public override async Task<U> Update<U, V>(int id, V entity) where U : class where V : class
        {
            var sourceEntity = await DpOrderContent.GetById(id);
            var updateEntity = BuildMapper<V, DtoOrderContentUpdate>().Map<DtoOrderContentUpdate>(entity);
            if (VerifyEntityUpdate(updateEntity.IdGamme, sourceEntity.IdGamme))
            {
                await RollBackStock(sourceEntity, updateEntity);
                sourceEntity.IdGamme = (int)updateEntity.IdGamme;
            }
            if (VerifyEntityUpdate(updateEntity.IdOrder, sourceEntity.IdOrder))
                sourceEntity.IdOrder = (int)updateEntity.IdOrder;
            if (VerifyEntityUpdate(updateEntity.Quantity, sourceEntity.Quantity))
            {
                Gamme gamme = await BsGamme.GetById<Gamme>(sourceEntity.IdGamme);
                gamme.IdProducer = 1;
                int diffStock = sourceEntity.Quantity - (int)updateEntity.Quantity;
                await BsGamme.Update<DtoGamme, DtoGammeUpdate>(sourceEntity.IdGamme, new DtoGammeUpdate() { Stock = (gamme.Stock + diffStock) });
                sourceEntity.Quantity = (int)updateEntity.Quantity;
            }
            return await base.Update<U, OrderContent>(id, sourceEntity);
        }

        private async Task RollBackStock(OrderContent result, DtoOrderContentUpdate entity)
        {
            DtoGamme oldGamme = await BsGamme.GetById<DtoGamme>(result.IdGamme);
            DtoGamme newGamme = await BsGamme.GetById<DtoGamme>((int)entity.IdGamme);
            await BsGamme.Update<DtoGamme, DtoGammeUpdate>((int)oldGamme.Id, BuildMapper<Gamme, DtoGammeUpdate>().Map<DtoGammeUpdate>(new Gamme() { Stock = oldGamme.Stock + result.Quantity }));
            await BsGamme.Update<DtoGamme, DtoGammeUpdate>((int)newGamme.Id, BuildMapper<Gamme, DtoGammeUpdate>().Map<DtoGammeUpdate>(new Gamme() { Stock = newGamme.Stock - (int)entity.Quantity }));
            result.Quantity = (int)entity.Quantity;
        }

        public override async Task<U> Add<U, V>(V entity) where U : class where V : class
        {
            DtoGamme gamme = await BsGamme.GetById<DtoGamme>(BuildMapper<V, DtoOrderContent>().Map<DtoOrderContent>(entity).IdGamme);
            if (gamme.Stock != 0)
                BsGamme.UpdateStock(BuildMapper<DtoGamme, Gamme>().Map<Gamme>(gamme), new DtoStockHisto() { Date = DateTime.Now, Stock = (int)gamme.Stock - BuildMapper<V, DtoOrderContent>().Map<DtoOrderContent>(entity).Quantity });
            return BuildMapper<OrderContent, U>().Map<U>(await DpOrderContent.Add(BuildMapper<V, OrderContent>().Map<OrderContent>(entity)));
        }
    }
}
