using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TicsaAPI.DAL.Models
{
    public partial class Orders : BasicElement
    {
        public DateTime OrderDate { get; set; }
        public int IdClient { get; set; }
    }
}
