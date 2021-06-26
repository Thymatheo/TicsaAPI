using TicsaAPI.BLL.DTO.GammeType;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class GammeTypeExtension {
        public static DtoGammeType ToDto(this GammeType gammeType) {
            return new DtoGammeType() {
                Id = gammeType.Id,
                Label = gammeType.Label
            };
        }

        public static DtoGammeTypeAdd ToDtoAdd(this GammeType gammeType) {
            return new DtoGammeTypeAdd() {
                Id = gammeType.Id,
                Label = gammeType.Label
            };
        }

        public static DtoGammeTypeUpdate ToDtoUpdate(this GammeType gammeType) {
            return new DtoGammeTypeUpdate() {
                Label = gammeType.Label
            };
        }
    }
}
