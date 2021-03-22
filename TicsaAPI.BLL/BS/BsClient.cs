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
            if (entity.Address != null)
                result.Address = entity.Address;
            if (entity.PostalCode != null)
                result.PostalCode = entity.PostalCode;
            if (entity.LastName != null)
                result.LastName = entity.LastName;
            if (entity.FirstName != null)
                result.FirstName = entity.FirstName;
            if (entity.CompagnieName != null)
                result.CompagnieName = entity.CompagnieName;
            if (entity.PhoneNumber != null)
                result.PhoneNumber = entity.PhoneNumber;
            if (entity.Email != null)
                result.Email = entity.Email;
            return await _dpClient.Update(result);
        }
    }
}
