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
            result.Address = entity.Address;
            result.PostalCode = entity.PostalCode;
            result.LastName = entity.LastName;
            result.FirstName = entity.FirstName;
            result.CompagnieName = entity.CompagnieName;
            result.PhoneNumber = entity.PhoneNumber;
            result.Email = entity.Email;
            return await _dpProducer.Update(result);
        }
    }
}
