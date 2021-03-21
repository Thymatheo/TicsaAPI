﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TicsaAPI.DAL.Models
{
    public partial class Gammes: BasicElement
    {
        public Gammes()
        {
            OrderContent = new HashSet<OrderContent>();
        }
        public string Label { get; set; }
        public string Description { get; set; }
        public string CostHisto { get; set; }
        public double Cost { get; set; }
        public int IdType { get; set; }
        public int Stock { get; set; }
        public string StockHisto { get; set; }

        public virtual GammeTypes IdTypeNavigation { get; set; }
        public virtual ICollection<OrderContent> OrderContent { get; set; }
    }
}