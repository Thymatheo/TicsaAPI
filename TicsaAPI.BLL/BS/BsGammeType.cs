using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.GammeType;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {
    public class BsGammeType : IBsGammeType {
        public IDpGammeType DpGammeType { get; set; }
        public BsGammeType(IDpGammeType dpGammeType) {
            DpGammeType = dpGammeType;
        }
        public async Task<IEnumerable<DtoGammeType>> GetAll() {
            List<DtoGammeType> result = new List<DtoGammeType>();
            (await DpGammeType.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoGammeType> GetById(int id) =>
            (await DpGammeType.GetById(id)).ToDto();

        public async Task<DtoGammeType> Update(int id, DtoGammeTypeUpdate entity) =>
            (await DpGammeType.Update(UpdateData(await DpGammeType.GetById(id), entity))).ToDto();

        private GammeType UpdateData(GammeType target, DtoGammeTypeUpdate source) {
            if (string.IsNullOrEmpty(source.Label))
                if (source.Label != target.Label)
                    target.Label = source.Label;
            return target;
        }

        public async Task<DtoGammeType> Remove(int id) =>
            (await DpGammeType.Remove(await DpGammeType.GetById(id))).ToDto();


        public async Task<DtoGammeTypeAdd> Add(GammeType entity) =>
            (await DpGammeType.Add(entity)).ToDtoAdd();

        public async Task AddRange(IEnumerable<GammeType> entityList) =>
            await DpGammeType.AddRange(entityList);

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<GammeType> entityToRemove = (await DpGammeType.GetAll()).ToList();
            await DpGammeType.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }
        public async Task UpdateRange(Dictionary<int, DtoGammeTypeUpdate> entityList) {
            List<GammeType> entityToUpdate = new List<GammeType>();
            IEnumerable<GammeType> entities = await DpGammeType.GetAll();
            foreach (KeyValuePair<int, DtoGammeTypeUpdate> entity in entityList)
                entityToUpdate.Add(UpdateData(entities.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            await DpGammeType.UpdateRange(entityToUpdate);
        }
    }
}
