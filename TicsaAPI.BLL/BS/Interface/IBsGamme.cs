using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsGamme : IBasicBs<Gammes>
    {
        Task<IEnumerable<Gammes>> GetGammesByIdType(int idType);
    }
}
