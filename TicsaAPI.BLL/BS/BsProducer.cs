using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Producer;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {
    public class BsProducer : IBsProducer {
        private IDpProducer DpProducer { get; set; }
        public BsProducer(IDpProducer dp) {
            DpProducer = dp;
        }
        public async Task<IEnumerable<DtoProducer>> GetAll() {
            List<DtoProducer> result = new List<DtoProducer>();
            (await DpProducer.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoProducer> GetById(int id) {
            return (await DpProducer.GetById(id)).ToDto();
        }

        public async Task<DtoProducer> Update(int id, DtoProducerUpdate entity) {
            return (await DpProducer.Update(UpdateData(await DpProducer.GetById(id), entity))).ToDto();
        }

        private Producer UpdateData(Producer target, DtoProducerUpdate source) {
            if (!string.IsNullOrEmpty(source.Address)) {
                if (source.Address != target.Address) {
                    target.Address = source.Address;
                }
            }

            if (!string.IsNullOrEmpty(source.CompagnieName)) {
                if (source.CompagnieName != target.CompagnieName) {
                    target.CompagnieName = source.CompagnieName;
                }
            }

            if (!string.IsNullOrEmpty(source.Email)) {
                if (source.Email != target.Email) {
                    target.Email = source.Email;
                }
            }

            if (!string.IsNullOrEmpty(source.FirstName)) {
                if (source.FirstName != target.FirstName) {
                    target.FirstName = source.FirstName;
                }
            }

            if (!string.IsNullOrEmpty(source.LastName)) {
                if (source.LastName != target.LastName) {
                    target.LastName = source.LastName;
                }
            }

            if (!string.IsNullOrEmpty(source.PhoneNumber)) {
                if (source.PhoneNumber != target.PhoneNumber) {
                    target.PhoneNumber = source.PhoneNumber;
                }
            }

            if (!string.IsNullOrEmpty(source.PostalCode)) {
                if (source.PostalCode != target.PostalCode) {
                    target.PostalCode = source.PostalCode;
                }
            }

            return target;
        }

        public async Task<DtoProducer> Remove(int id) {
            return (await DpProducer.Remove(await DpProducer.GetById(id))).ToDto();
        }

        public async Task<DtoProducerAdd> Add(Producer entity) {
            return (await DpProducer.Add(entity)).ToDtoAdd();
        }

        public async Task AddRange(IEnumerable<Producer> entityList) {
            await DpProducer.AddRange(entityList);
        }

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<Producer> entityToRemove = (await DpProducer.GetAll()).ToList();
            await DpProducer.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }
        public async Task UpdateRange(Dictionary<int, DtoProducerUpdate> entityList) {
            List<Producer> entityToUpdate = new List<Producer>();
            IEnumerable<Producer> entities = await DpProducer.GetAll();
            foreach (KeyValuePair<int, DtoProducerUpdate> entity in entityList) {
                entityToUpdate.Add(UpdateData(entities.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            }

            await DpProducer.UpdateRange(entityToUpdate);
        }
    }
}
