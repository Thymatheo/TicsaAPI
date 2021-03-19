using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicsaAPI.DAL.Model
{
    [Table("OrderContent")]
    public class OrderContent
    {
        [Key]
        public int IdOrderContent { get; set; }
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        [ForeignKey("Gamme")]
        public int IdGamme { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Gamme Gamme { get; set; }
    }
}
