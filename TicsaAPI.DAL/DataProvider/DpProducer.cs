using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpProducer : BasicDp<Producer>, IDpProducer
    {
        public DpProducer(DbSet<Producer> table, TicsaContext db) : base(table, db)
        {
        }
    }
}
