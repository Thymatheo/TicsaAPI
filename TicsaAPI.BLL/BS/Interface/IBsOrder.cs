using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsOrder : IBasicBs<Order>
    {
        Task<IEnumerable<Order>> GetByIdClient(int idClient);
    }
}
