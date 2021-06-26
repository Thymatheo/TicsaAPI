using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class OrderContentExtension {
        public static DtoOrderContent ToDto(this OrderContent order) {
            return new DtoOrderContent() {
                Id = order.Id,
                IdGamme = order.IdGamme,
                IdOrder = order.IdOrder,
                Quantity = order.Quantity
            };
        }

        public static DtoOrderContentAdd ToDtoAdd(this OrderContent order) {
            return new DtoOrderContentAdd() {
                Id = order.Id,
                IdGamme = order.IdGamme,
                IdOrder = order.IdOrder,
                Quantity = order.Quantity
            };
        }

        public static DtoOrderContentUpdate ToDtoUpdate(this OrderContent order) {
            return new DtoOrderContentUpdate() {
                IdGamme = order.IdGamme,
                IdOrder = order.IdOrder,
                Quantity = order.Quantity
            };
        }
    }
}
