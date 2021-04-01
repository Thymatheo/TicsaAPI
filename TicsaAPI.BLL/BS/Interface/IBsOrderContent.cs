using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsOrderContent : IBasicBs<OrderContent>
    {
        Task<IEnumerable<DtoOrderContent>> GetByIdOrder(int idOrder);
    }
}
