using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGamme : BasicBs<Gamme>, IBsGamme
    {
        private IDpGamme _dpGamme { get; set; }

        public BsGamme(IDpGamme dpGamme) : base(dpGamme)
        {
            _dpGamme = dpGamme;
        }

        public async Task<IEnumerable<Gamme>> GetGammesByIdType(int idType)
        {
            return await _dpGamme.GetGammesByIdType(idType);
        }

        public override Task<Gamme> Update(int id, Gamme entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
