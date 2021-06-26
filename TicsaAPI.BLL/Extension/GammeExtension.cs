using TicsaAPI.BLL.DTO.Gamme;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class GammeExtension {
        public static DtoGamme ToDto(this Gamme gamme) {
            return new DtoGamme() {
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
        }

        public static DtoGammeAdd ToDtoAdd(this Gamme gamme) {
            return new DtoGammeAdd() {
                Id = gamme.Id,
                Label = gamme.Label,
                Cost = gamme.Cost,
                Stock = gamme.Stock,
                Description = gamme.Description,
                IdProducer = gamme.IdProducer,
                IdType = gamme.IdType
            };
        }

        public static DtoGammeUpdate ToDtoUpdate(this Gamme gamme) {
            return new DtoGammeUpdate() {
                Label = gamme.Label,
                Cost = gamme.Cost,
                Stock = gamme.Stock,
                Description = gamme.Description,
                IdProducer = gamme.IdProducer,
                IdType = gamme.IdType
            };
        }
    }
}
