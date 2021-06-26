using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider {
    public class DpProducer : BasicDp<Producer>, IDpProducer {
        public DpProducer(TicsaContext db) : base(db.Producer, db) {
        }
    }
}
