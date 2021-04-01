using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsClient : BasicBs<Client>, IBsClient
    {
        private readonly IDpClient DpClient;
        public BsClient(IDpClient dp) : base(dp)
        {
            DpClient = dp;
        }
        public override async Task<U> Update<U, V>(int id, V entity) where U : class where V : class
        {
            var sourceEntity = await DpClient.GetById(id);
            var updateEntity = BuildMapper<V, DtoClientUpdate>().Map<DtoClientUpdate>(entity);
            if (VerifyEntityUpdate(updateEntity.FirstName, sourceEntity.FirstName))
                sourceEntity.FirstName = updateEntity.FirstName;
            if (VerifyEntityUpdate(updateEntity.LastName, sourceEntity.LastName))
                sourceEntity.LastName = updateEntity.LastName;
            if (VerifyEntityUpdate(updateEntity.CompagnieName, sourceEntity.CompagnieName))
                sourceEntity.CompagnieName = updateEntity.CompagnieName;
            if (VerifyEntityUpdate(updateEntity.Address, sourceEntity.Address))
                sourceEntity.Address = updateEntity.Address;
            if (VerifyEntityUpdate(updateEntity.Email, sourceEntity.Email))
                sourceEntity.Email = updateEntity.Email;
            if (VerifyEntityUpdate(updateEntity.PhoneNumber, sourceEntity.PhoneNumber))
                sourceEntity.PhoneNumber = updateEntity.PhoneNumber;
            if (VerifyEntityUpdate(updateEntity.PostalCode, sourceEntity.PostalCode))
                sourceEntity.PostalCode = updateEntity.PostalCode;
            return await base.Update<U, Client>(id, sourceEntity);
        }
    }
}
