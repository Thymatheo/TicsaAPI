using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.OrderContent {
    public class DtoOrderContent : BasicElement {
        public int IdOrder { get; set; }
        public int IdGamme { get; set; }
        public int Quantity { get; set; }
    }
}
