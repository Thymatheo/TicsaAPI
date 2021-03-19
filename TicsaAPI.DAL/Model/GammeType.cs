using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicsaAPI.DAL.Model
{
    [Table("GammeType")]
    public class GammeType
    {
        [Key]
        public int IdType { get; set; }
        public string Label { get; set; }
    }
}
