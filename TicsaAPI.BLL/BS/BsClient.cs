using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsClient : BasicBs<Clients>, IBsClient
    {
        private IDpClient _dpGamme { get; set; }
        public BsClient(IDpClient dp) : base(dp)
        {
            _dpGamme = dp;
        }
    }
}
