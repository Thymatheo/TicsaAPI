﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BasicBs<T> : IBasicBs<T> where T : BasicElement
    {
        public IBasicDp<T> Dp { get; set; }

        public BasicBs(IBasicDp<T> dp)
        {
            Dp = dp;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Dp.GetAll();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Dp.GetById(id);
        }

        public virtual async Task<T> Update(T entity)
        {
            return await Dp.Update(entity);
        }

        public virtual async Task<T> Remove(T entity)
        {
            return await Dp.Remove(entity);
        }

        public virtual async Task<T> Add(T entity)
        {
            return await Dp.Add(entity);
        }
    }
}
