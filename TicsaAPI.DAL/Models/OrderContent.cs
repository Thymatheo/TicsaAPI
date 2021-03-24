// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TicsaAPI.DAL.Models
{
    public partial class OrderContent : BasicElement
    {
        public int IdOrder { get; set; }
        public int IdGamme { get; set; }
        public int Quantity { get; set; }

        public virtual Gamme? IdGammeNavigation { get; set; }
        public virtual Order? IdOrderNavigation { get; set; }
    }
}
