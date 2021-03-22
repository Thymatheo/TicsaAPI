using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsClient : BasicBs<Client>, IBsClient
    {
        private IDpClient _dpGamme { get; set; }
        public BsClient(IDpClient dp) : base(dp)
        {
            _dpGamme = dp;
        }

        public override Task<Client> Update(int id, Client entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
