using TicsaAPI.BLL.DTO.Producer;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class ProducerExtension {
        public static DtoProducer ToDto(this Producer Producer) {
            return new DtoProducer() {
                Id = Producer.Id,
                Address = Producer.Address,
                CompagnieName = Producer.CompagnieName,
                Email = Producer.Email,
                FirstName = Producer.FirstName,
                LastName = Producer.LastName,
                PhoneNumber = Producer.PhoneNumber,
                PostalCode = Producer.PostalCode
            };
        }

        public static DtoProducerAdd ToDtoAdd(this Producer Producer) {
            return new DtoProducerAdd() {
                Id = Producer.Id,
                Address = Producer.Address,
                CompagnieName = Producer.CompagnieName,
                Email = Producer.Email,
                FirstName = Producer.FirstName,
                LastName = Producer.LastName,
                PhoneNumber = Producer.PhoneNumber,
                PostalCode = Producer.PostalCode
            };
        }

        public static DtoProducerUpdate ToDtoUpdate(this Producer Producer) {
            return new DtoProducerUpdate() {
                Address = Producer.Address,
                CompagnieName = Producer.CompagnieName,
                Email = Producer.Email,
                FirstName = Producer.FirstName,
                LastName = Producer.LastName,
                PhoneNumber = Producer.PhoneNumber,
                PostalCode = Producer.PostalCode
            };
        }
    }
}
