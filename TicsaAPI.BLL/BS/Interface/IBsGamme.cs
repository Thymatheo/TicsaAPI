using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.DTO;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsGamme : IBasicBs<Gamme>
    {
        Task<IEnumerable<Gamme>> GetGammesByIdType(int idType);
        Gamme UpdateCost(Gamme entity, DtoCostHisto newHisto);
        Gamme UpdateStock(Gamme entity, DtoStockHisto newHisto);
    }
}
