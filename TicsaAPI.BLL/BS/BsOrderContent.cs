using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrderContent : BasicBs<OrderContent>, IBsOrderContent
    {
        private IDpOrderContent _dpOrderContent { get; set; }
        public BsOrderContent(IDpOrderContent dp) : base(dp)
        {
            _dpOrderContent = dp;
        }
        public async Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder)
        {
            return await _dpOrderContent.GetByIdOrder(idOrder);
        }

        public override async Task<OrderContent> Update(int id, OrderContent entity)
        {
            var result = await _dpOrderContent.GetById(id);
            result.IdGamme = entity.IdGamme;
            result.IdOrder = entity.IdOrder;
            result.Quantity = entity.Quantity;
            return await _dpOrderContent.Update(result);
        }
    }
}
