using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsOrderContent : IBasicBs<OrderContent>
    {
        Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder);
    }
}
