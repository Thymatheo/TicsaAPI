using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Model;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsGamme
    {
        Task<IEnumerable<Gamme>> GetAllGamme();
        Task<IEnumerable<Gamme>> GetGammesByIdType(int idType);
        Task<Gamme> GetGammeById(int idGamme);
        Task<Gamme> UpdateGamme(Gamme gamme);
        Task<Gamme> RemoveGamme(Gamme gamme);
        Task<Gamme> AddGamme(Gamme gamme);
    }
}
