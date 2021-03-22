using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpOrderContent : BasicDp<OrderContent>, IDpOrderContent
    {
        public DpOrderContent(TicsaContext db) : base(db.OrderContent, db) { }

        public async Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder)
        {
            return await Table.Where(x => x.IdOrder == idOrder).ToListAsync();
        }
    }
}
