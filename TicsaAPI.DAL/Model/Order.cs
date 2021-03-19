using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicsaAPI.DAL.Model
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        [ForeignKey("Client")]
        public int IdClient { get; set; }
        public DateTime Date { get; set; }
        public virtual Client Client { get; set; }
    }
}
