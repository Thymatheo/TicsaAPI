using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGamme : BasicBs<Gamme>, IBsGamme
    {
        private IDpGamme DpGamme { get; set; }

        public BsGamme(IDpGamme dp) : base(dp)
        {
            DpGamme = dp;
        }

        public async Task<IEnumerable<DtoGamme>> GetGammesByIdType(int idType)
        {
            Mapper mapper = BuildMapper<Gamme, DtoGamme>();
            List<DtoGamme> result = new List<DtoGamme>();
            foreach (Gamme entity in await DpGamme.GetGammesByIdType(idType))
                result.Add(mapper.Map<DtoGamme>(entity));
            return result;
        }
        public async Task<IEnumerable<DtoGamme>> GetGammesByIdProducer(int idProducer)
        {
            Mapper mapper = BuildMapper<Gamme, DtoGamme>();
            List<DtoGamme> result = new List<DtoGamme>();
            foreach (Gamme entity in await DpGamme.GetGammesByIdType(idProducer))
                result.Add(mapper.Map<DtoGamme>(entity));
            return result;
        }

        public override async Task<U> Update<U, V>(int id, V entity) where U : class where V : class
        {
            var sourceEntity = await DpGamme.GetById(id);
            var updateEntity = BuildMapper<V, DtoGammeUpdate>().Map<DtoGammeUpdate>(entity);
            if (VerifyEntityUpdate(updateEntity.Label, sourceEntity.Label))
                sourceEntity.Label = updateEntity.Label;
            if (VerifyEntityUpdate(updateEntity.Description, sourceEntity.Description))
                sourceEntity.Description = updateEntity.Description;
            if (VerifyEntityUpdate(updateEntity.IdProducer, sourceEntity.IdProducer))
                sourceEntity.IdProducer = (int)updateEntity.IdProducer;
            if (VerifyEntityUpdate(updateEntity.IdType, sourceEntity.IdType))
                sourceEntity.IdType = (int)updateEntity.IdType;
            if (VerifyEntityUpdate(updateEntity.Cost, sourceEntity.Cost))
                sourceEntity = UpdateCost(sourceEntity, new DtoCostHisto() { Date = DateTime.Now, Cost = (double)updateEntity.Cost });
            if (VerifyEntityUpdate(updateEntity.Stock, sourceEntity.Stock))
                sourceEntity = UpdateStock(sourceEntity, new DtoStockHisto() { Date = DateTime.Now, Stock = (int)updateEntity.Stock });
            return await base.Update<U, Gamme>(id, sourceEntity);

        }

        public override async Task<U> Add<U, V>(V entity) where U : class where V : class
        {
            return BuildMapper<Gamme, U>().Map<U>(await DpGamme.Add(InitHisto(BuildMapper<V, Gamme>().Map<Gamme>(entity))));
        }

        private Gamme InitHisto(Gamme entity)
        {
            List<DtoCostHisto> costHisto = new List<DtoCostHisto>();
            List<DtoStockHisto> stockHisto = new List<DtoStockHisto>();
            if (entity.Cost != null)
                costHisto.Add(new DtoCostHisto() { Date = DateTime.Now, Cost = (double)entity.Cost });
            if (entity.Stock != null)
                stockHisto.Add(new DtoStockHisto() { Date = DateTime.Now, Stock = (int)entity.Stock });
            entity.StockHisto = JsonConvert.SerializeObject(stockHisto);
            entity.CostHisto = JsonConvert.SerializeObject(costHisto);
            return entity;

        }

        public Gamme UpdateCost(Gamme entity, DtoCostHisto newHisto)
        {
            List<DtoCostHisto> costHisto = JsonConvert.DeserializeObject<List<DtoCostHisto>>(entity.CostHisto);
            costHisto.Add(newHisto);
            entity.CostHisto = JsonConvert.SerializeObject(costHisto);
            entity.Cost = newHisto.Cost;
            return entity;
        }
        public Gamme UpdateStock(Gamme entity, DtoStockHisto newHisto)
        {
            List<DtoStockHisto> stockHisto = JsonConvert.DeserializeObject<List<DtoStockHisto>>(entity.StockHisto);
            stockHisto.Add(newHisto);
            entity.StockHisto = JsonConvert.SerializeObject(stockHisto);
            entity.Stock = newHisto.Stock;
            return entity;
        }
    }
}
