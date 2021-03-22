using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGamme : BasicBs<Gammes>, IBsGamme
    {
        private IDpGamme _dpGamme { get; set; }

        public BsGamme(IDpGamme dpGamme) : base(dpGamme)
        {
            _dpGamme = dpGamme;
        }

        public async Task<IEnumerable<Gammes>> GetGammesByIdType(int idType)
        {
            return await _dpGamme.GetGammesByIdType(idType);
        }
    }
}
