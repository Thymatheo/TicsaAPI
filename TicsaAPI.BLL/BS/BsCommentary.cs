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
        private IDpCommentary _dpCommentary { get; set; }
        public BsCommentary(IDpCommentary dp) : base(dp)
        {
            _dpCommentary = dp;
        }

        public override async Task<Commentary> Update(int id, Commentary entity)
        {
            var result = await _dpCommentary.GetById(id);
            result.Content = entity.Content;
            result.IdClient = entity.IdClient;
            return await _dpCommentary.Update(result);
        }
    }
}
