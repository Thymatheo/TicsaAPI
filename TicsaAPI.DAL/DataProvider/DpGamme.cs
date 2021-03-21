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
    public class DpGamme : BasicDp<Gammes>, IDpGamme
    {
        public DpGamme(TicsaContext db) : base(db.Gammes, db) { }

        public async Task<IEnumerable<Gammes>> GetGammesByIdType(int idType)
        {
            return await Table.Where(x => x.IdType == idType).ToListAsync();
        }

    }
}
