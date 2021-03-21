using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGamme : IBsGamme
    {
        private IDpGamme _dpGamme { get; set; }

        public BsGamme(IDpGamme dpGamme)
        {
            _dpGamme = dpGamme;
        }

        public async Task<IEnumerable<Gammes>> GetAllGamme()
        {
            return await _dpGamme.GetAll();
        }

        public async Task<IEnumerable<Gammes>> GetGammesByIdType(int idType)
        {
            return await _dpGamme.GetGammesByIdType(idType);
        }

        public async Task<Gammes> GetGammeById(int idGamme)
        {
            return await _dpGamme.GetById(idGamme);
        }

        public async Task<Gammes> UpdateGamme(Gammes gamme)
        {
            return await _dpGamme.Update(gamme);
        }

        public async Task<Gammes> RemoveGamme(Gammes gamme)
        {
            return await _dpGamme.Remove(gamme);
        }

        public async Task<Gammes> AddGamme(Gammes gamme)
        {
            return await _dpGamme.Add(gamme);
        }
    }
}
