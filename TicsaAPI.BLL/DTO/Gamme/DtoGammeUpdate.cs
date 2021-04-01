using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Gamme
{
    public class DtoGammeUpdate : BasicElement
    {
        public int? Id { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public double? Cost { get; set; }
        public int? IdType { get; set; }
        public int? Stock { get; set; }
        public int? IdProducer { get; set; }
    }
}
