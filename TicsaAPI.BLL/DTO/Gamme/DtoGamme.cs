using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Gamme {
    public class DtoGamme : BasicElement {
        public string Label { get; set; }
        public string Description { get; set; }
        public string CostHisto { get; set; }
        public double Cost { get; set; }
        public int IdType { get; set; }
        public int Stock { get; set; }
        public string StockHisto { get; set; }
        public int IdProducer { get; set; }
    }
}
