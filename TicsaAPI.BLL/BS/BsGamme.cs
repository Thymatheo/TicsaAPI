using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {

    public class BsGamme : IBsGamme {
        private IDpGamme DpGamme { get; set; }

        public BsGamme(IDpGamme dpGamme) {
            DpGamme = dpGamme;
        }
        public async Task<IEnumerable<DtoGamme>> GetAll() {
            List<DtoGamme> result = new List<DtoGamme>();
            (await DpGamme.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoGamme> GetById(int id) {
            return (await DpGamme.GetById(id)).ToDto();
        }

        public async Task<IEnumerable<DtoGamme>> GetGammesByIdType(int idType) {
            List<DtoGamme> result = new List<DtoGamme>();
            (await DpGamme.GetGammesByIdType(idType)).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<IEnumerable<DtoGamme>> GetGammesByIdProducer(int idProducer) {
            List<DtoGamme> result = new List<DtoGamme>();
            (await DpGamme.GetGammesByIdType(idProducer)).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoGamme> Update(int id, DtoGammeUpdate entity) {
            return (await DpGamme.Update(UpdateData(await DpGamme.GetById(id), entity))).ToDto();
        }

        public async Task<DtoGammeAdd> Add(Gamme entity) {
            return (await DpGamme.Add(InitHisto(entity))).ToDtoAdd();
        }

        private Gamme InitHisto(Gamme entity) {
            List<DtoCostHisto> costHisto = new List<DtoCostHisto>();
            List<DtoStockHisto> stockHisto = new List<DtoStockHisto>();
            if (entity.Cost != null) {
                costHisto.Add(new DtoCostHisto() { Date = DateTime.Now, Cost = (double)entity.Cost });
            }

            if (entity.Stock != null) {
                stockHisto.Add(new DtoStockHisto() { Date = DateTime.Now, Stock = entity.Stock });
            }

            entity.StockHisto = JsonConvert.SerializeObject(stockHisto);
            entity.CostHisto = JsonConvert.SerializeObject(costHisto);
            return entity;

        }

        public Gamme UpdateCost(Gamme entity, DtoCostHisto newHisto) {
            List<DtoCostHisto> costHisto = JsonConvert.DeserializeObject<List<DtoCostHisto>>(entity.CostHisto);
            costHisto.Add(newHisto);
            entity.CostHisto = JsonConvert.SerializeObject(costHisto);
            entity.Cost = newHisto.Cost;
            return entity;
        }
        public Gamme UpdateStock(Gamme entity, DtoStockHisto newHisto) {
            List<DtoStockHisto> stockHisto = JsonConvert.DeserializeObject<List<DtoStockHisto>>(entity.StockHisto);
            stockHisto.Add(newHisto);
            entity.StockHisto = JsonConvert.SerializeObject(stockHisto);
            entity.Stock = newHisto.Stock;
            return entity;
        }
        public async Task<DtoGamme> Remove(int id) {
            return (await DpGamme.Remove(await DpGamme.GetById(id))).ToDto();
        }

        public async Task AddRange(IEnumerable<Gamme> entityList) {
            List<Gamme> gammes = new List<Gamme>();
            entityList.ToList().ForEach(x => gammes.Add(InitHisto(x)));
            await DpGamme.AddRange(gammes);
        }

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<Gamme> entityToRemove = (await DpGamme.GetAll()).ToList();
            await DpGamme.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }

        public async Task UpdateRange(Dictionary<int, DtoGammeUpdate> entityList) {
            List<Gamme> entityToUpdate = new List<Gamme>();
            IEnumerable<Gamme> entities = await DpGamme.GetAll();
            foreach (KeyValuePair<int, DtoGammeUpdate> entity in entityList) {
                entityToUpdate.Add(UpdateData(entityToUpdate.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            }

            await DpGamme.UpdateRange(entityToUpdate);
        }

        private Gamme UpdateData(Gamme target, DtoGammeUpdate source) {
            if (!string.IsNullOrEmpty(source.Description)) {
                if (source.Description != target.Description) {
                    target.Description = source.Description;
                }
            }

            if (!string.IsNullOrEmpty(source.Label)) {
                if (source.Label != target.Label) {
                    target.Label = source.Label;
                }
            }

            if (source.IdProducer != 0) {
                if (source.IdProducer != target.IdProducer) {
                    target.IdProducer = (int)source.IdProducer;
                }
            }

            if (source.IdType != 0) {
                if (source.IdType != target.IdType) {
                    target.IdType = (int)source.IdType;
                }
            }

            if (source.Cost != 0) {
                if (source.Cost != target.Cost) {
                    target = UpdateCost(target, new DtoCostHisto() { Date = DateTime.Now, Cost = (double)source.Cost });
                }
            }

            if (source.Stock != 0) {
                if (source.Stock != target.Stock) {
                    target = UpdateStock(target, new DtoStockHisto() { Date = DateTime.Now, Stock = (int)source.Stock });
                }
            }

            return target;
        }
    }
}
