using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class GammeExtension {
        public static DtoGamme ToDto(this Gamme gamme) => new DtoGamme() {
            Id = gamme.Id,
            Label = gamme.Label,
            Cost = gamme.Cost,
            Stock = gamme.Stock,
            CostHisto = gamme.CostHisto,
            StockHisto = gamme.StockHisto,
            Description = gamme.Description,
            IdProducer = gamme.IdProducer,
            IdType = gamme.IdType
        };

        public static DtoGammeAdd ToDtoAdd(this Gamme gamme) => new DtoGammeAdd() {
            Id = gamme.Id,
            Label = gamme.Label,
            Cost = gamme.Cost,
            Stock = gamme.Stock,
            Description = gamme.Description,
            IdProducer = gamme.IdProducer,
            IdType = gamme.IdType
        };

        public static DtoGammeUpdate ToDtoUpdate(this Gamme gamme) => new DtoGammeUpdate() {
            Label = gamme.Label,
            Cost = gamme.Cost,
            Stock = gamme.Stock,
            Description = gamme.Description,
            IdProducer = gamme.IdProducer,
            IdType = gamme.IdType
        };
    }
}
