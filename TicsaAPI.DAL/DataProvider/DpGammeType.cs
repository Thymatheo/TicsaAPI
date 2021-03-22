using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpGammeType : BasicDp<GammeType>, IDpGammeType
    {
        public DpGammeType(TicsaContext db) : base(db.GammeType, db) { }
    }
}
