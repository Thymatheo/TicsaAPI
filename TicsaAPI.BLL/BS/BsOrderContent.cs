using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrderContent : BasicBs<OrderContent>, IBsOrderContent
    {
        public BsOrderContent(IBasicDp<OrderContent> dp) : base(dp)
        {
        }
    }
}
