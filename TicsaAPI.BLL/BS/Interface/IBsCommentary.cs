using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.Commentary;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsCommentary {
        Task<DtoCommentaryAdd> Add(Commentary entity);
        Task AddRange(IEnumerable<Commentary> entityList);
        Task<IEnumerable<DtoCommentary>> GetAll();
        Task<DtoCommentary> GetById(int id);
        Task<IEnumerable<DtoCommentary>> GetByIdClient(int idClient);
        Task<DtoCommentary> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoCommentary> Update(int id, DtoCommentaryUpdate entity);
        Task UpdateRange(Dictionary<int, DtoCommentaryUpdate> entityList);
    }
}