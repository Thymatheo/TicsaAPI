using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.DTO.Order;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class OrderExtension {
        public static DtoOrder ToDto(this Order order) => new DtoOrder() {
            Id = order.Id,
            IdClient = order.IdClient,
            OrderDate = order.OrderDate
        };
        public static DtoOrderAdd ToDtoAdd(this Order order) => new DtoOrderAdd() {
            Id = order.Id,
            IdClient = order.IdClient,
            OrderDate = order.OrderDate
        };

        public static DtoOrderUpdate ToDtoUpdate(this Order order) => new DtoOrderUpdate() {
            IdClient = order.IdClient,
            OrderDate = order.OrderDate
        };
    }
}
