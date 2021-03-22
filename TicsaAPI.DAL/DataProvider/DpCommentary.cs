using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider
{
    public class DpCommentary : BasicDp<Commentary>, IDpCommentary
    {
        public DpCommentary(TicsaContext db) : base(db.Commentary, db)
        {
        }
    }
}
