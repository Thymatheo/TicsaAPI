using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsOrderContent {
        Task<DtoOrderContentAdd> Add(OrderContent entity);
        Task AddRange(IEnumerable<OrderContent> entityList);
        Task<IEnumerable<DtoOrderContent>> GetAll();
        Task<DtoOrderContent> GetById(int id);
        Task<IEnumerable<DtoOrderContent>> GetByIdOrder(int idOrder);
        Task<DtoOrderContent> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoOrderContent> Update(int id, DtoOrderContentUpdate entity);
        Task UpdateRange(Dictionary<int, DtoOrderContentUpdate> entityList);
    }
}