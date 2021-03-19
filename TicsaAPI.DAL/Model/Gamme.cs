using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicsaAPI.DAL.Model
{
    [Table("Gamme")]
    public class Gamme
    {
        [Key]
        public int IdGamme { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string CostHisto { get; set; }
        [ForeignKey("GammeType")]
        public int IdType { get; set; }
        public int Stock { get; set; }
        public string StockHisto { get; set; }
        public virtual GammeType GammeType { get; set; }
    }
}
