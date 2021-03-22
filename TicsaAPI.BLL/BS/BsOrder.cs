using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrder : BasicBs<Order>, IBsOrder
    {
        public IDpOrder _dpOrder { get; set; }
        public BsOrder(IDpOrder dp) : base(dp)
        {
            _dpOrder = dp;
        }

        public override async Task<Order> Update(int id, Order entity)
        {
            var result = await _dpOrder.GetById(id);
            result.IdClient = entity.IdClient;
            result.OrderDate = entity.OrderDate;
            return await _dpOrder.Update(result);
        }
    }
}
