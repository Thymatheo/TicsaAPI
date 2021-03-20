using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider.Interface
{
    public interface IDpGamme : IBasicDp<Gammes>
    {
        Task<IEnumerable<Gammes>> GetGammesByIdType(int idType);
    }
}
