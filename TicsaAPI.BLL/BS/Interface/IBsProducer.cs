using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.Producer;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsProducer {

        Task<DtoProducerAdd> Add(Producer entity);
        Task AddRange(IEnumerable<Producer> entityList);
        Task<IEnumerable<DtoProducer>> GetAll();
        Task<DtoProducer> GetById(int id);
        Task<DtoProducer> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoProducer> Update(int id, DtoProducerUpdate entity);
        Task UpdateRange(Dictionary<int, DtoProducerUpdate> entityList);
    }
}