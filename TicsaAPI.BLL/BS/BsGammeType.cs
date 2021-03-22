using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGammeType : BasicBs<GammeType>, IBsGammeType
    {
        private IDpGammeType _dpGammeType { get; set; }
        public BsGammeType(IDpGammeType dpGammeType) : base(dpGammeType)
        {
            _dpGammeType = dpGammeType;
        }

        public override async Task<GammeType> Update(int id, GammeType entity)
        {
            var result = await _dpGammeType.GetById(id);
            result.Label = entity.Label;
            return await _dpGammeType.Update(result);
        }
    }
}
