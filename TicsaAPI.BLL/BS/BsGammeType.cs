﻿using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGammeType : BasicBs<GammeType>, IBsGammeType
    {
        private IDpGammeType DpGammeType { get; set; }
        public BsGammeType(IDpGammeType dpGammeType) : base(dpGammeType)
        {
            DpGammeType = dpGammeType;
        }

        public override async Task<GammeType> Update(int id, GammeType entity)
        {
            var result = await DpGammeType.GetById(id);
            if (entity.Label != null)
                result.Label = entity.Label;
            return await DpGammeType.Update(result);
        }
    }
}
