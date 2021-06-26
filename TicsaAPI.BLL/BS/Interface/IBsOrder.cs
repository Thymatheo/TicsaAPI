using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Order;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsOrder {
        Task<DtoOrderAdd> Add(Order entity);
        Task AddRange(IEnumerable<Order> entityList);
        Task<IEnumerable<DtoOrder>> GetAll();
        Task<DtoOrder> GetById(int id);
        Task<IEnumerable<DtoOrder>> GetByIdClient(int idClient);
        Task<DtoOrder> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoOrder> Update(int id, DtoOrderUpdate entity);
        Task UpdateRange(Dictionary<int, DtoOrderUpdate> entityList);
    }
}