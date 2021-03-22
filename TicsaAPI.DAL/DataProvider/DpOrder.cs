using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpOrder : BasicDp<Order>, IDpOrder
    {
        public DpOrder(TicsaContext db) : base(db.Order, db) { }
    }
}
