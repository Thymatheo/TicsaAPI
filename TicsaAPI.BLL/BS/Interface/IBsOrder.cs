using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.Order;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsOrder : IBasicBs<Order>
    {
        Task<IEnumerable<DtoOrder>> GetByIdClient(int idClient);
    }
}
