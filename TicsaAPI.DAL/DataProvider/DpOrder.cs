using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpOrder : BasicDp<Orders>, IDpOrder
    {
        public DpOrder(DbSet<Orders> table, TicsaContext db) : base(table, db)
        {
        }
    }
}
