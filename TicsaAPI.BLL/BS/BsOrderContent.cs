using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO;
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
        public async Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder)
        {
            return await DpOrderContent.GetByIdOrder(idOrder);
        }

        public override async Task<OrderContent> Update(int id, OrderContent entity)
        {
            var result = await DpOrderContent.GetById(id);
            if (entity.IdGamme != 0)
                if (result.IdGamme != entity.IdGamme)
                {
                    await RollBackStock(result, entity);
                    result.IdGamme = entity.IdGamme;
                }
            if (entity.IdOrder != 0)
                if (result.IdOrder != entity.IdOrder)
                    result.IdOrder = entity.IdOrder;
            if (entity.Quantity != 0)
                if (result.Quantity != entity.Quantity)
                {
                    Gamme gamme = await BsGamme.GetById(result.IdGamme);
                    int diffStock = result.Quantity - entity.Quantity;
                    await BsGamme.Update(result.IdGamme, new Gamme() { Stock = (gamme.Stock + diffStock) });
                    result.Quantity = entity.Quantity;
                }
            return await DpOrderContent.Update(result);
        }

        private async Task RollBackStock(OrderContent result, OrderContent entity)
        {
            Gamme oldGamme = await BsGamme.GetById(result.IdGamme);
            Gamme newGamme = await BsGamme.GetById(entity.IdGamme);
            await BsGamme.Update(oldGamme.Id, new Gamme() { Stock = oldGamme.Stock + result.Quantity });
            await BsGamme.Update(newGamme.Id, new Gamme() { Stock = newGamme.Stock - entity.Quantity });
            result.Quantity = entity.Quantity;
        }

        public override async Task<OrderContent> Add(OrderContent entity)
        {
            var gamme = await BsGamme.GetById(entity.IdGamme);
            if (gamme.Stock != null)
                BsGamme.UpdateStock(gamme, new DtoStockHisto() { Date = DateTime.Now, Stock = (int)gamme.Stock - entity.Quantity });
            return await DpOrderContent.Add(entity);
        }
    }
}
