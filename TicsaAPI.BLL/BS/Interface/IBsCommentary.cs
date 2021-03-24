using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsCommentary : IBasicBs<Commentary>
    {
        Task<IEnumerable<Commentary>> GetByIdClient(int idClient);
    }
}
