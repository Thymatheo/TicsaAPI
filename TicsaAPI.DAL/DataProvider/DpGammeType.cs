﻿using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpGammeType : BasicDp<GammeTypes>
    {
        public DpGammeType(TicsaContext db) : base(db.GammeTypes, db) { 
        }
    }
}
