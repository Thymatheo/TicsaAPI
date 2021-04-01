using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Order
{
    public class DtoOrderAdd : BasicElement
    {
        public int? Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int IdClient { get; set; }
    }
}
