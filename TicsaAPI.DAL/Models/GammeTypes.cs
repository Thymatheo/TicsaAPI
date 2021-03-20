using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TicsaAPI.DAL.Models
{
    public partial class GammeTypes : BasicElement
    {
        public GammeTypes()
        {
            Gammes = new HashSet<Gammes>();
        }

        public string Label { get; set; }

        public virtual ICollection<Gammes> Gammes { get; set; }
    }
}
