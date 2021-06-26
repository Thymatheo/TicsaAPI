using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {
    public class BsOrderContent : IBsOrderContent {
        private IDpOrderContent DpOrderContent { get; set; }
        private IBsGamme BsGamme { get; set; }
        private IDpGamme DpGamme { get; set; }

        public BsOrderContent(IDpOrderContent dp, IBsGamme bsGamme, IDpGamme dpGamme) {
            DpOrderContent = dp;
            BsGamme = bsGamme;
            DpGamme = dpGamme;
        }

        public async Task<IEnumerable<DtoOrderContent>> GetAll() {
            List<DtoOrderContent> result = new List<DtoOrderContent>();
            (await DpOrderContent.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoOrderContent> GetById(int id) {
            return (await DpOrderContent.GetById(id)).ToDto();
        }

        public async Task<DtoOrderContent> Update(int id, DtoOrderContentUpdate entity) {
            return (await DpOrderContent.Update(await UpdateData(await DpOrderContent.GetById(id), entity))).ToDto();
        }

        private async Task<OrderContent> UpdateData(OrderContent target, DtoOrderContentUpdate source) {
            if (source.IdGamme != 0) {
                if (target.IdGamme != source.IdGamme) {
                    target = await RollBackStock(target, source);
                    target.IdGamme = (int)source.IdGamme;
                }
            }

            if (source.IdOrder != 0) {
                if (target.IdOrder != source.IdOrder) {
                    target.IdOrder = (int)source.IdOrder;
                }
            }

            if (source.Quantity != 0) {
                if (target.Quantity != source.Quantity) {
                    Gamme gamme = await DpGamme.GetById(target.IdGamme);
                    int diffStock = target.Quantity - (int)source.Quantity;
                    await BsGamme.Update(target.IdGamme, new DtoGammeUpdate() { Stock = (gamme.Stock + diffStock) });
                    target.Quantity = (int)source.Quantity;
                }
            }

            return target;
        }

        public async Task<DtoOrderContent> Remove(int id) {
            return (await DpOrderContent.Remove(await DpOrderContent.GetById(id))).ToDto();
        }

        public async Task AddRange(IEnumerable<OrderContent> entityList) {
            await DpOrderContent.AddRange(entityList);
        }

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<OrderContent> entityToRemove = (await DpOrderContent.GetAll()).ToList();
            await DpOrderContent.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }
        public async Task UpdateRange(Dictionary<int, DtoOrderContentUpdate> entityList) {
            List<OrderContent> entityToUpdate = new List<OrderContent>();
            IEnumerable<OrderContent> entities = await DpOrderContent.GetAll();
            foreach (KeyValuePair<int, DtoOrderContentUpdate> entity in entityList) {
                entityToUpdate.Add(await UpdateData(entities.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            }

            await DpOrderContent.UpdateRange(entityToUpdate);
        }
        public async Task<IEnumerable<DtoOrderContent>> GetByIdOrder(int idOrder) {
            List<DtoOrderContent> result = new List<DtoOrderContent>();
            (await DpOrderContent.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        private async Task<OrderContent> RollBackStock(OrderContent result, DtoOrderContentUpdate entity) {
            Gamme oldGamme = await DpGamme.GetById(result.IdGamme);
            Gamme newGamme = await DpGamme.GetById((int)entity.IdGamme);
            await BsGamme.Update(oldGamme.Id, new DtoGammeUpdate() { Stock = oldGamme.Stock + result.Quantity });
            await BsGamme.Update(newGamme.Id, new DtoGammeUpdate() { Stock = newGamme.Stock - entity.Quantity });
            result.Quantity = (int)entity.Quantity;
            return result;
        }

        public async Task<DtoOrderContentAdd> Add(OrderContent entity) {
            Gamme gamme = await DpGamme.GetById(entity.IdGamme);
            if (gamme.Stock != 0) {
                BsGamme.UpdateStock(gamme, new DtoStockHisto() { Date = DateTime.Now, Stock = gamme.Stock - entity.Quantity });
            }

            return (await DpOrderContent.Add(entity)).ToDtoAdd();
        }
    }
}
