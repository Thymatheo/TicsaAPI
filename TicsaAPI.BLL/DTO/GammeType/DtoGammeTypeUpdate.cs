﻿using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.GammeType
{
    public class DtoGammeTypeUpdate : BasicElement
    {
        public int? Id { get; set; }
        public string? Label { get; set; }
    }
}
