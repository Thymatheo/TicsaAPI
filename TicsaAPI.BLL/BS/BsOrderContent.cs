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
        private IDpOrderContent _dpOrderContent { get; set; }

        private IBsGamme _bsGamme { get; set; }

        public BsOrderContent(IDpOrderContent dp, IBsGamme bsGamme) : base(dp)
        {
            _dpOrderContent = dp;
            _bsGamme = bsGamme;
        }
        public async Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder)
        {
            return await _dpOrderContent.GetByIdOrder(idOrder);
        }

        public override async Task<OrderContent> Update(int id, OrderContent entity)
        {
            var result = await _dpOrderContent.GetById(id);
            if (entity.IdGamme != 0)
                result.IdGamme = entity.IdGamme;
            if (entity.IdOrder != 0)
                result.IdOrder = entity.IdOrder;
            if (entity.Quantity != 0)
                result.Quantity = entity.Quantity;
            return await _dpOrderContent.Update(result);
        }

        public override async Task<OrderContent> Add(OrderContent entity)
        {
            var gamme = await _bsGamme.GetById(entity.IdGamme);
            gamme.Stock = gamme.Stock - entity.Quantity;
            _bsGamme.UpdateStock(gamme, new DtoStockHisto() { Date = DateTime.Now, Stock = gamme.Stock });
            return await _dpOrderContent.Add(entity);
        }
    }
}
