using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.DTO.GammeType;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class GammeTypeExtension {
        public static DtoGammeType ToDto(this GammeType gammeType) => new DtoGammeType() {
            Id = gammeType.Id,
            Label = gammeType.Label
        };
        public static DtoGammeTypeAdd ToDtoAdd(this GammeType gammeType) => new DtoGammeTypeAdd() {
            Id = gammeType.Id,
            Label = gammeType.Label
        };

        public static DtoGammeTypeUpdate ToDtoUpdate(this GammeType gammeType) => new DtoGammeTypeUpdate() {
            Label = gammeType.Label
        };
    }
}
