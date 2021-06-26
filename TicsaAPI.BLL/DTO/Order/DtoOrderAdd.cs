using System;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Order {
    public class DtoOrderAdd : BasicElement {
        public DateTime OrderDate { get; set; }
        public int IdClient { get; set; }
    }
}
