using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.DTO.OrderContent;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class OrderContentExtension {
        public static DtoOrderContent ToDto(this OrderContent order) => new DtoOrderContent() {
            Id = order.Id,
            IdClient = order.IdClient,
            OrderDate = order.OrderDate
        };
        public static DtoOrderContentAdd ToDtoAdd(this OrderContent order) => new DtoOrderContentAdd() {
            Id = order.Id,
            IdClient = order.IdClient,
            OrderDate = order.OrderDate
        };

        public static DtoOrderContentUpdate ToDtoUpdate(this OrderContent order) => new DtoOrderContentUpdate() {
            IdClient = order.IdClient,
            OrderDate = order.OrderDate
        };
    }
}
