using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsClient : BasicBs<Client>, IBsClient
    {
        private IDpClient DpClient { get; set; }
        private IBsOrder BsOrder { get; set; }
        public BsClient(IDpClient dp, IBsOrder bsOrder) : base(dp)
        {
            DpClient = dp;
            BsOrder = bsOrder;
        }

        public override async Task<Client> Update(int id, Client entity)
        {
            var result = await DpClient.GetById(id);
            if (entity.Address != null)
                result.Address = entity.Address;
            if (entity.PostalCode != null)
                result.PostalCode = entity.PostalCode;
            if (entity.LastName != null)
                result.LastName = entity.LastName;
            if (entity.FirstName != null)
                result.FirstName = entity.FirstName;
            if (entity.CompagnieName != null)
                result.CompagnieName = entity.CompagnieName;
            if (entity.PhoneNumber != null)
                result.PhoneNumber = entity.PhoneNumber;
            if (entity.Email != null)
                result.Email = entity.Email;
            return await DpClient.Update(result);
        }
        public override async Task<Client> Add(Client entity)
        {
            if (entity.Order != null)
                if (entity.Order.Any())
                {
                    List<Order> orders = new List<Order>();
                    orders.AddRange(entity.Order);
                    entity.Order = new List<Order>();
                    var result = await DpClient.Add(entity);
                    foreach (Order order in orders)
                    {
                        order.IdClient = result.Id;
                        await BsOrder.Add(order);
                    }
                    return result;
                }
            return await DpClient.Add(entity);
        }
    }
}
