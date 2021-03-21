using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrder : BasicBs<Orders>, IBsOrder
    {
        public IDpOrder _dpOrder { get; set; }
        public BsOrder(IDpOrder dp) : base(dp)
        {
            _dpOrder = dp;
        }
    }
}
