using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider.Interface
{
    public interface IDpGamme : IBasicDp<Gamme>
    {
        Task<IEnumerable<Gamme>> GetGammesByIdType(int idType);
        Task<IEnumerable<Gamme>> GetGammesByIdProducer(int idProducer);
    }
}
