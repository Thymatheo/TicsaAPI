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
        public BsProducer(IBasicDp<Producer> dp) : base(dp)
        {
        }

        public override Task<Producer> Update(int id, Producer entity)
        {
            throw new NotImplementedException();
        }
    }
}
