using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpClient : BasicDp<Client>, IDpClient
    {
        public DpClient(TicsaContext db) : base(db.Client, db) { }
    }
}
