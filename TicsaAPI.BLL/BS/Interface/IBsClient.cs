using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsClient {
        IDpClient DpClient { get; set; }

        Task<DtoClientAdd> Add(Client entity);
        Task AddRange(IEnumerable<Client> entityList);
        Task<IEnumerable<DtoClient>> GetAll();
        Task<DtoClient> GetById(int id);
        Task<DtoClient> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoClient> Update(int id, DtoClientUpdate entity);
        Task UpdateRange(Dictionary<int, DtoClientUpdate> entityList);
    }
}