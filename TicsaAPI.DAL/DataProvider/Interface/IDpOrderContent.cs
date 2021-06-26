using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider.Interface {
    public interface IDpOrderContent : IBasicDp<OrderContent> {
        Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder);
    }
}
