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
        public IBsOrderContent BsOrderContent { get; set; }
        public BsOrder(IDpOrder dp, IBsOrderContent bsOrderContent) : base(dp)
        {
            DpOrder = dp;
            BsOrderContent = bsOrderContent;
        }

        public override async Task<Order> Update(int id, Order entity)
        {
            var result = await DpOrder.GetById(id);
            if (entity.IdClient != 0)
                result.IdClient = entity.IdClient;
            if (entity.OrderDate != null)
                result.OrderDate = entity.OrderDate;
            return await DpOrder.Update(result);
        }
        public override async Task<Order> Add(Order entity)
        {
            if (entity.OrderContent != null)
                if (entity.OrderContent.ToList().Any())
                {
                    List<OrderContent> orderContent = new List<OrderContent>();
                    orderContent.AddRange(entity.OrderContent);
                    entity.OrderContent = new List<OrderContent>();
                    var result = await DpOrder.Add(entity);
                    foreach (OrderContent content in orderContent)
                    {
                        content.IdOrder = result.Id;
                        await BsOrderContent.Add(content);
                    }
                    return result;
                }
            return await DpOrder.Add(entity);
        }
    }
}
