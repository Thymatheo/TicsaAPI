﻿using Newtonsoft.Json;
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
            if (entity.Label != null)
                result.Label = entity.Label;
            if (entity.Description != null)
                result.Description = entity.Description;
            if (entity.Cost != result.Cost)
                result = UpdateCost(result, new DtoCostHisto() { Date = DateTime.Now, Cost = entity.Cost });
            if (entity.Stock != result.Stock)
                result = UpdateStock(result, new DtoStockHisto() { Date = DateTime.Now, Stock = entity.Stock });
            if (entity.IdProducer != 0)
                result.IdProducer = entity.IdProducer;
            if (entity.IdProducer != 0)
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

        public Gamme UpdateCost(Gamme entity, DtoCostHisto newHisto)
        {
            List<DtoCostHisto> costHisto = JsonConvert.DeserializeObject<List<DtoCostHisto>>(entity.CostHisto);
            costHisto.Add(newHisto);
            entity.CostHisto = JsonConvert.SerializeObject(costHisto);
            entity.Cost = entity.Cost;
            return entity;
        }
        public Gamme UpdateStock(Gamme entity, DtoStockHisto newHisto)
        {
            List<DtoStockHisto> stockHisto = JsonConvert.DeserializeObject<List<DtoStockHisto>>(entity.StockHisto);
            stockHisto.Add(newHisto);
            entity.StockHisto = JsonConvert.SerializeObject(stockHisto);
            entity.Stock = entity.Stock;
            return entity;
        }
    }
}
