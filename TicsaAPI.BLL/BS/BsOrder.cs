using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrder : BasicBs<Order>, IBsOrder
    {
        public IDpOrder DpOrder { get; set; }
        public BsOrder(IDpOrder dp, IBsOrderContent bsOrderContent) : base(dp)
        {
            DpOrder = dp;
        }
        public async Task<IEnumerable<Order>> GetByIdClient(int idClient)
        {
            return await DpOrder.GetByIdClient(idClient);
        }
    }
}
