using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider {
    public class DpOrder : BasicDp<Order>, IDpOrder {
        public DpOrder(TicsaContext db) : base(db.Order, db) { }

        public async Task<IEnumerable<Order>> GetByIdClient(int idClient) {
            return await Table.Where(x => x.IdClient == idClient).ToListAsync();
        }
    }
}
