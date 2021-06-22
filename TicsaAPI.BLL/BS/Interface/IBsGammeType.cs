using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO.GammeType;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsGammeType {
        IDpGammeType DpGammeType { get; set; }

        Task<DtoGammeTypeAdd> Add(GammeType entity);
        Task AddRange(IEnumerable<GammeType> entityList);
        Task<IEnumerable<DtoGammeType>> GetAll();
        Task<DtoGammeType> GetById(int id);
        Task<DtoGammeType> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoGammeType> Update(int id, DtoGammeTypeUpdate entity);
        Task UpdateRange(Dictionary<int, DtoGammeTypeUpdate> entityList);
    }
}