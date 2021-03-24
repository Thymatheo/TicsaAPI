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
        private IDpCommentary DpCommentary { get; set; }
        public BsCommentary(IDpCommentary dp) : base(dp)
        {
            DpCommentary = dp;
        }

        public override async Task<Commentary> Update(int id, Commentary entity)
        {
            var result = await DpCommentary.GetById(id);
            if (entity.CommentaryContent != null)
                result.CommentaryContent = entity.CommentaryContent;
            if (entity.IdClient != 0)
                result.IdClient = entity.IdClient;
            return await DpCommentary.Update(result);
        }

        public async Task<IEnumerable<Commentary>> GetByIdClient(int idClient)
        {
            return await DpCommentary.GetByIdClient(idClient);
        }
    }
}
