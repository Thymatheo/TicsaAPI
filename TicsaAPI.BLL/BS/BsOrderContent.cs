using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {
    public class BsOrderContent {
        private IDpOrderContent DpOrderContent { get; set; }
        private IBsGamme BsGamme { get; set; }
        private IDpGamme DpGamme { get; set; }

        public BsOrderContent(IDpOrderContent dp, IBsGamme bsGamme, IDpGamme dpGamme) {
            DpOrderContent = dp;
            BsGamme = bsGamme;
            DpGamme = dpGamme;
        }
        public async Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder) {
            return await DpOrderContent.GetByIdOrder(idOrder);
        }

        public async Task<DtoOrderContent> Update(int id, DtoOrderContentUpdate entity) {
            var result = await DpOrderContent.GetById(id);
            if (entity.IdGamme != 0)
                if (result.IdGamme != entity.IdGamme) {
                    await RollBackStock(result, entity);
                    result.IdGamme = entity.IdGamme;
                }
            if (entity.IdOrder != 0)
                if (result.IdOrder != entity.IdOrder)
                    result.IdOrder = entity.IdOrder;
            if (entity.Quantity != 0)
                if (result.Quantity != entity.Quantity) {
                    Gamme gamme = await DpGamme.GetById(result.IdGamme);
                    int diffStock = result.Quantity - entity.Quantity;
                    await BsGamme.Update(result.IdGamme, new DtoGammeUpdate() { Stock = (gamme.Stock + diffStock) });
                    result.Quantity = entity.Quantity;
                }
            return (await DpOrderContent.Update(result)).ToDto();
        }

        private async Task RollBackStock(OrderContent result, OrderContent entity) {
            Gamme oldGamme = await DpGamme.GetById(result.IdGamme);
            Gamme newGamme = await DpGamme.GetById(entity.IdGamme);
            await BsGamme.Update(oldGamme.Id, new DtoGammeUpdate() { Stock = oldGamme.Stock + result.Quantity });
            await BsGamme.Update(newGamme.Id, new DtoGammeUpdate() { Stock = newGamme.Stock - entity.Quantity });
            result.Quantity = entity.Quantity;
        }

        public async Task<DtoOrderContentAdd> Add(OrderContent entity) {
            Gamme gamme = await DpGamme.GetById(entity.IdGamme);
            if (gamme.Stock != null)
                BsGamme.UpdateStock(gamme, new DtoStockHisto() { Date = DateTime.Now, Stock = (int)gamme.Stock - entity.Quantity });
            return await DpOrderContent.Add(entity).ToDto();
        }
    }
}
