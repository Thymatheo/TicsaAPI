using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGammeType : BasicBs<GammeType>, IBsGammeType
    {
        public BsGammeType(IDpGammeType dpGammeType) : base(dpGammeType)
        {
        }
    }
}
