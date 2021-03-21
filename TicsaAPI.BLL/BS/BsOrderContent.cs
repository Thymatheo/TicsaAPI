using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsOrderContent : BasicBs<OrderContent>, IBsOrderContent
    {
        private IDpOrderContent _dpOrderContent { get; set; }
        public BsOrderContent(IDpOrderContent dp) : base(dp)
        {
            _dpOrderContent = dp;
        }
        public async Task<IEnumerable<OrderContent>> GetByIdOrder(int idOrder)
        {
            return await _dpOrderContent.GetByIdOrder(idOrder);
        }
    }
}
