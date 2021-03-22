using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpGammeType : BasicDp<GammeTypes>, IDpGammeType
    {
        public DpGammeType(TicsaContext db) : base(db.GammeTypes, db) { }
    }
}
