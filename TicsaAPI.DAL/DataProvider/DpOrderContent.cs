using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpOrderContent : BasicDp<OrderContent>, IDpOrderContent
    {
        public DpOrderContent(DbSet<OrderContent> table, TicsaContext db) : base(table, db)
        {
        }
    }
}
