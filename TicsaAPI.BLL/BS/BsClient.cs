using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Clients;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {
    public class BsClient : IBsClient {
        public IDpClient DpClient { get; set; }
        public BsClient(IDpClient dp) {
            DpClient = dp;
        }
        public async Task<IEnumerable<DtoClient>> GetAll() {
            List<DtoClient> result = new List<DtoClient>();
            (await DpClient.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoClient> GetById(int id) =>
            (await DpClient.GetById(id)).ToDto();

        public async Task<DtoClient> Update(int id, DtoClientUpdate entity) =>
            (await DpClient.Update(UpdateData(await DpClient.GetById(id), entity))).ToDto();

        private Client UpdateData(Client target, DtoClientUpdate source) {
            if (string.IsNullOrEmpty(source.Address))
                if (source.Address != target.Address)
                    target.Address = source.Address;
            if (string.IsNullOrEmpty(source.CompagnieName))
                if (source.CompagnieName != target.CompagnieName)
                    target.CompagnieName = source.CompagnieName;
            if (string.IsNullOrEmpty(source.Email))
                if (source.Email != target.Email)
                    target.Email = source.Email;
            if (string.IsNullOrEmpty(source.FirstName))
                if (source.FirstName != target.FirstName)
                    target.FirstName = source.FirstName;
            if (string.IsNullOrEmpty(source.LastName))
                if (source.LastName != target.LastName)
                    target.LastName = source.LastName;
            if (string.IsNullOrEmpty(source.PhoneNumber))
                if (source.PhoneNumber != target.PhoneNumber)
                    target.PhoneNumber = source.PhoneNumber;
            if (string.IsNullOrEmpty(source.PostalCode))
                if (source.PostalCode != target.PostalCode)
                    target.PostalCode = source.PostalCode;
            return target;
        }

        public async Task<DtoClient> Remove(int id) =>
            (await DpClient.Remove(await DpClient.GetById(id))).ToDto();


        public async Task<DtoClientAdd> Add(Client entity) =>
            (await DpClient.Add(entity)).ToDtoAdd();

        public async Task AddRange(IEnumerable<Client> entityList) =>
            await DpClient.AddRange(entityList);

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<Client> entityToRemove = (await DpClient.GetAll()).ToList();
            await DpClient.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }
        public async Task UpdateRange(Dictionary<int, DtoClientUpdate> entityList) {
            List<Client> entityToUpdate = new List<Client>();
            IEnumerable<Client> entities = await DpClient.GetAll();
            foreach (KeyValuePair<int, DtoClientUpdate> entity in entityList)
                entityToUpdate.Add(UpdateData(entities.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            await DpClient.UpdateRange(entityToUpdate);
        }
    }
}
