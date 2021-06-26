﻿using System;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Order {
    public class DtoOrderUpdate : BasicElement {
        public DateTime? OrderDate { get; set; }
        public int? IdClient { get; set; }
    }
}
