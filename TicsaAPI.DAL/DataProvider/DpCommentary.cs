using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider {
    public class DpCommentary : BasicDp<Commentary>, IDpCommentary {
        public DpCommentary(TicsaContext db) : base(db.Commentary, db) {
        }

        public async Task<IEnumerable<Commentary>> GetByIdClient(int idClient) {
            return await Table.Where(x => x.IdClient == idClient).ToListAsync();
        }
    }
}
