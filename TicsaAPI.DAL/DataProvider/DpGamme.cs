using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider {
    public class DpGamme : BasicDp<Gamme>, IDpGamme {
        public DpGamme(TicsaContext db) : base(db.Gamme, db) { }

        public async Task<IEnumerable<Gamme>> GetGammesByIdType(int idType) {
            return await Table.Where(x => x.IdType == idType).ToListAsync();
        }
        public async Task<IEnumerable<Gamme>> GetGammesByIdProducer(int idProducer) {
            return await Table.Where(x => x.IdProducer == idProducer).ToListAsync();
        }

    }
}
