﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS
{
    public class BsGammeType : BasicBs<GammeTypes>, IBsGammeType
    {
        private IDpGammeType _dpGammeType { get; set; }
        public BsGammeType(IDpGammeType dpGammeType) : base(dpGammeType)
        {
            _dpGammeType = dpGammeType;
        }
    }
}