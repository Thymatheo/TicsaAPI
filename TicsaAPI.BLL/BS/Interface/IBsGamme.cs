using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface {
    public interface IBsGamme {
        Task<DtoGammeAdd> Add(Gamme entity);
        Task AddRange(IEnumerable<Gamme> entityList);
        Task<IEnumerable<DtoGamme>> GetAll();
        Task<DtoGamme> GetById(int id);
        Task<IEnumerable<DtoGamme>> GetGammesByIdProducer(int idProducer);
        Task<IEnumerable<DtoGamme>> GetGammesByIdType(int idType);
        Task<DtoGamme> Remove(int id);
        Task RemoveRange(IEnumerable<int> entityList);
        Task<DtoGamme> Update(int id, DtoGammeUpdate entity);
        Gamme UpdateCost(Gamme entity, DtoCostHisto newHisto);
        Task UpdateRange(Dictionary<int, DtoGammeUpdate> entityList);
        Gamme UpdateStock(Gamme entity, DtoStockHisto newHisto);
    }
}