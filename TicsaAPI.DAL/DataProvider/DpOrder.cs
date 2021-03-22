﻿using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpOrder : BasicDp<Orders>, IDpOrder
    {
        public DpOrder(TicsaContext db) : base(db.Orders, db) { }
    }
}
