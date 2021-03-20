using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpGamme : IDpGamme
    {
        private TicsaContext _db { get; set; }
        public DpGamme(TicsaContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Gammes>> GetAllGamme()
        {
            return await _db.Gammes.ToListAsync();
        }

        public async Task<IEnumerable<Gammes>> GetGammesByIdType(int idType)
        {
            return await _db.Gammes.Where(x => x.IdType == idType).ToListAsync();
        }

        public async Task<Gammes> GetGammeById(int idGamme)
        {
            return await _db.Gammes.Where(x => x.Id == idGamme).FirstOrDefaultAsync();
        }

        public async Task<Gammes> UpdateGamme(Gammes gamme)
        {
            var result = _db.Gammes.Update(gamme);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Gammes> RemoveGamme(Gammes gamme)
        {
            var result = _db.Gammes.Remove(gamme);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Gammes> AddGamme(Gammes gamme)
        {
            var result = _db.Gammes.Add(gamme);
            await _db.SaveChangesAsync();
            return result.Entity;
        }
    }
}
