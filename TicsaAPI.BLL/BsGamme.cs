using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Model;

namespace TicsaAPI.BLL
{
    public class BsGamme : IBsGamme
    {
        private IDpGamme _dpGamme { get; set; }

        public BsGamme(IDpGamme dpGamme)
        {
            _dpGamme = dpGamme;
        }

        public async Task<IEnumerable<Gamme>> GetAllGamme()
        {
            return await _dpGamme.GetAllGamme();
        }

        public async Task<IEnumerable<Gamme>> GetGammesByIdType(int idType)
        {
            return await _dpGamme.GetGammesByIdType(idType);
        }

        public async Task<Gamme> GetGammeById(int idGamme)
        {
            return await _dpGamme.GetGammeById(idGamme);
        }

        public async Task<Gamme> UpdateGamme(Gamme gamme)
        {
            return await _dpGamme.UpdateGamme(gamme);
        }

        public async Task<Gamme> RemoveGamme(Gamme gamme)
        {
            return await _dpGamme.RemoveGamme(gamme);
        }

        public async Task<Gamme> AddGamme(Gamme gamme)
        {
            return await _dpGamme.AddGamme(gamme);
        }
    }
}
