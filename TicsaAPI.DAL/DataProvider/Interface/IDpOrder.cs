using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider.Interface
{
    public interface IDpOrder : IBasicDp<Order>
    {
        Task<IEnumerable<Order>> GetByIdClient(int idClient);
    }
}
