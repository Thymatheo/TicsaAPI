using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;
using TicsaAPI.BLL.DTO.Order;

namespace TicsaAPI.BLL.BS
{
    public class BsOrder : BasicBs<Order>, IBsOrder
    {
        public IDpOrder DpOrder { get; set; }
        public BsOrder(IDpOrder dp, IBsOrderContent bsOrderContent) : base(dp)
        {
            DpOrder = dp;
        }
        public async Task<IEnumerable<DtoOrder>> GetByIdClient(int idClient)
        {
            Mapper mapper = BuildMapper<Order, DtoOrder>();
            List<DtoOrder> result = new List<DtoOrder>();
            foreach (Order entity in await DpOrder.GetByIdClient(idClient))
                result.Add(mapper.Map<DtoOrder>(entity));
            return result;
        }
    }
}
