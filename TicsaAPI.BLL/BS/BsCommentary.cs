﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.BLL.DTO.Commentary;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsCommentary : BasicBs<Commentary>, IBsCommentary
    {
        private IDpCommentary DpCommentary { get; set; }
        public BsCommentary(IDpCommentary dp) : base(dp)
        {
            DpCommentary = dp;
        }

        public async Task<IEnumerable<DtoCommentary>> GetByIdClient(int idClient)
        {
            Mapper mapper = BuildMapper<Commentary, DtoCommentary>();
            List<DtoCommentary> result = new List<DtoCommentary>();
            foreach (Commentary entity in await DpCommentary.GetByIdClient(idClient))
                result.Add(mapper.Map<DtoCommentary>(entity));
            return result;
        }
    }
}
