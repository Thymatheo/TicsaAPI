using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Commentary;
using TicsaAPI.BLL.Extension;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS {
    public class BsCommentary : IBsCommentary {
        private IDpCommentary DpCommentary { get; set; }
        public BsCommentary(IDpCommentary dp) {
            DpCommentary = dp;
        }

        public async Task<IEnumerable<DtoCommentary>> GetAll() {
            List<DtoCommentary> result = new List<DtoCommentary>();
            (await DpCommentary.GetAll()).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }

        public async Task<DtoCommentary> GetById(int id) =>
            (await DpCommentary.GetById(id)).ToDto();

        public async Task<DtoCommentary> Update(int id, DtoCommentaryUpdate entity) =>
            (await DpCommentary.Update(UpdateData(await DpCommentary.GetById(id), entity))).ToDto();

        private Commentary UpdateData(Commentary target, DtoCommentaryUpdate source) {
            if (string.IsNullOrEmpty(source.CommentaryContent))
                if (source.CommentaryContent != target.CommentaryContent)
                    target.CommentaryContent = source.CommentaryContent;
            if (source.CommentaryDate != null)
                if (source.CommentaryDate != target.CommentaryDate)
                    target.CommentaryDate = (DateTime)source.CommentaryDate;
            if (source.IdClient != null)
                if (source.IdClient != target.IdClient)
                    target.IdClient = (int)source.IdClient;
            return target;
        }

        public async Task<DtoCommentary> Remove(int id) =>
            (await DpCommentary.Remove(await DpCommentary.GetById(id))).ToDto();


        public async Task<DtoCommentaryAdd> Add(Commentary entity) =>
            (await DpCommentary.Add(entity)).ToDtoAdd();

        public async Task AddRange(IEnumerable<Commentary> entityList) =>
            await DpCommentary.AddRange(entityList);

        public async Task RemoveRange(IEnumerable<int> entityList) {
            List<Commentary> entityToRemove = (await DpCommentary.GetAll()).ToList();
            await DpCommentary.RemoveRange(entityToRemove.Where(x => entityList.Contains(x.Id)));
        }
        public async Task UpdateRange(Dictionary<int, DtoCommentaryUpdate> entityList) {
            List<Commentary> entityToUpdate = new List<Commentary>();
            IEnumerable<Commentary> entities = await DpCommentary.GetAll();
            foreach (KeyValuePair<int, DtoCommentaryUpdate> entity in entityList)
                entityToUpdate.Add(UpdateData(entities.Where(x => x.Id == entity.Key).FirstOrDefault(), entity.Value));
            await DpCommentary.UpdateRange(entityToUpdate);
        }
        public async Task<IEnumerable<DtoCommentary>> GetByIdClient(int idClient) {
            List<DtoCommentary> result = new List<DtoCommentary>();
            (await DpCommentary.GetByIdClient(idClient)).ToList().ForEach(x => result.Add(x.ToDto()));
            return result;
        }
    }
}
