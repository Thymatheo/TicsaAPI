using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicsaAPI.DAL.Model
{
    public abstract class BasicElement
    {
        [Key]
        public int Id { get; set; }

    }
}
