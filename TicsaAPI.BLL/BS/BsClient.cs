using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsClient : BasicBs<Client>, IBsClient
    {
        private IDpClient _dpClient { get; set; }
        public BsClient(IDpClient dp) : base(dp)
        {
            _dpClient = dp;
        }

        public override async Task<Client> Update(int id, Client entity)
        {
            var result = await _dpClient.GetById(id);
            result.Address = entity.Address;
            result.PostalCode = entity.PostalCode;
            result.LastName = entity.LastName;
            result.FirstName = entity.FirstName;
            result.CompagnieName = entity.CompagnieName;
            result.PhoneNumber = entity.PhoneNumber;
            result.Email = entity.Email;
            return await _dpClient.Update(result);
        }
    }
}
