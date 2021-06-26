using System.Collections.Generic;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider.Interface {
    public interface IDpCommentary : IBasicDp<Commentary> {
        Task<IEnumerable<Commentary>> GetByIdClient(int idClient);
    }
}
