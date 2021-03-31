using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsGamme : IBasicBs<Gamme>
    {
        Task<IEnumerable<DtoGamme>> GetGammesByIdProducer(int idProducer);
        Task<IEnumerable<DtoGamme>> GetGammesByIdType(int idType);
        Gamme UpdateCost(Gamme entity, DtoCostHisto newHisto);
        Gamme UpdateStock(Gamme entity, DtoStockHisto newHisto);
    }
}
