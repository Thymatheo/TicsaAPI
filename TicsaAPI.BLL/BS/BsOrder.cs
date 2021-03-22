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
            throw new System.NotImplementedException();
        }
    }
}
