using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGamme : BasicBs<Gamme>, IBsGamme
    {
        private IDpGamme _dpGamme { get; set; }

        public BsGamme(IDpGamme dpGamme) : base(dpGamme)
        {
            _dpGamme = dpGamme;
        }

        public async Task<IEnumerable<Gamme>> GetGammesByIdType(int idType)
        {
            return await _dpGamme.GetGammesByIdType(idType);
        }

        public override async Task<Gamme> Update(int id, Gamme entity)
        {

            var result = await _dpGamme.GetById(id);
            result.Label = entity.Label;
            result.Description = entity.Description;
            result = _UpdateCost(result, entity);
            result = _UpdateStock(result, entity);
            result.IdProducer = entity.IdProducer;
            result.IdType = entity.IdType;
            return await _dpGamme.Update(result);
        }

        public override async Task<Gamme> Add(Gamme entity)
        {
            var result = _InitHisto(entity);
            return await Dp.Add(result);
        }

        private Gamme _InitHisto(Gamme entity)
        {
            List<DtoCostHisto> costHisto = new List<DtoCostHisto>();
            List<DtoStockHisto> stockHisto = new List<DtoStockHisto>();
            costHisto.Add(new DtoCostHisto() { Date = DateTime.Now, Cost = entity.Cost });
            stockHisto.Add(new DtoStockHisto() { Date = DateTime.Now, Stock = entity.Stock });
            entity.StockHisto = JsonConvert.SerializeObject(stockHisto);
            entity.CostHisto = JsonConvert.SerializeObject(costHisto);
            return entity;

        }

        private Gamme _UpdateCost(Gamme result, Gamme entity)
        {
            List<DtoCostHisto> costHisto = JsonConvert.DeserializeObject<List<DtoCostHisto>>(entity.CostHisto);
            costHisto.Add(new DtoCostHisto() { Date = DateTime.Now, Cost = entity.Cost });
            result.CostHisto = JsonConvert.SerializeObject(costHisto);
            result.Cost = entity.Cost;
            return result;
        }
        private Gamme _UpdateStock(Gamme result, Gamme entity)
        {
            List<DtoStockHisto> stockHisto = JsonConvert.DeserializeObject<List<DtoStockHisto>>(entity.StockHisto);
            stockHisto.Add(new DtoStockHisto() { Date = DateTime.Now, Stock = entity.Stock });
            result.StockHisto = JsonConvert.SerializeObject(stockHisto);
            result.Stock = entity.Stock;
            return result;
        }
    }
}
