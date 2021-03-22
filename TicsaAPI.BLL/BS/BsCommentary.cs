using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsCommentary : BasicBs<Commentary>, IBsCommentary
    {
        public BsCommentary(IBasicDp<Commentary> dp) : base(dp)
        {
        }

        public override Task<Commentary> Update(int id, Commentary entity)
        {
            throw new NotImplementedException();
        }
    }
}
