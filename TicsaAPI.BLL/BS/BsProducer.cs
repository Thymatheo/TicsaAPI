using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsProducer : BasicBs<Producer>, IBsProducer
    {
        private IDpProducer _dpProducer;

        public BsProducer(IDpProducer dp) : base(dp)
        {
            _dpProducer = dp;
        }

        public override async Task<Producer> Update(int id, Producer entity)
        {
            var result = await _dpProducer.GetById(id);
            if (entity.Address != null)
                result.Address = entity.Address;
            if (entity.PostalCode != null)
                result.PostalCode = entity.PostalCode;
            if (entity.LastName != null)
                result.LastName = entity.LastName;
            if (entity.FirstName != null)
                result.FirstName = entity.FirstName;
            if (entity.CompagnieName != null)
                result.CompagnieName = entity.CompagnieName;
            if (entity.PhoneNumber != null)
                result.PhoneNumber = entity.PhoneNumber;
            if (entity.Email != null)
                result.Email = entity.Email;
            return await _dpProducer.Update(result);
        }
    }
}
