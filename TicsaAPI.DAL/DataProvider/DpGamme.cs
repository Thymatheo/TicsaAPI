using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Model;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpGamme : IDpGamme
    {
        private TicsaContext _db { get; set; }
        public DpGamme(TicsaContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Gamme>> GetAllGamme()
        {
            return await _db.Gammes.ToListAsync();
        }

        public async Task<IEnumerable<Gamme>> GetGammesByIdType(int idType)
        {
            return await _db.Gammes.Where(x => x.IdType == idType).ToListAsync();
        }

        public async Task<Gamme> GetGammeById(int idGamme)
        {
            return await _db.Gammes.Where(x => x.IdGamme == idGamme).FirstOrDefaultAsync();
        }

        public async Task<Gamme> UpdateGamme(Gamme gamme)
        {
            var result = _db.Gammes.Update(gamme);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Gamme> RemoveGamme(Gamme gamme)
        {
            var result = _db.Gammes.Remove(gamme);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Gamme> AddGamme(Gamme gamme)
        {
            var result = _db.Gammes.Add(gamme);
            await _db.SaveChangesAsync();
            return result.Entity;
        }
    }
}
