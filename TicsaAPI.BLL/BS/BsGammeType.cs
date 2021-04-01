using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.GammeType;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGammeType : BasicBs<GammeType>, IBsGammeType
    {
        private readonly IDpGammeType DpGammeType;
        public BsGammeType(IDpGammeType dp) : base(dp)
        {
            DpGammeType = dp;
        }
        public override async Task<U> Update<U, V>(int id, V entity) where U : class where V : class
        {
            var sourceEntity = await DpGammeType.GetById(id);
            var updateEntity = BuildMapper<V, DtoGammeTypeUpdate>().Map<DtoGammeTypeUpdate>(entity);
            if (VerifyEntityUpdate(updateEntity.Label, sourceEntity.Label))
                sourceEntity.Label = updateEntity.Label;
            return await base.Update<U, GammeType>(id, sourceEntity);
        }
    }
}
