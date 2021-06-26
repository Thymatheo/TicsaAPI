﻿using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TicsaAPI.DAL.Models {
    public partial class Producer : BasicElement {
        public Producer() {
            Gamme = new HashSet<Gamme>();
        }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? CompagnieName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PostalCode { get; set; }

        public virtual ICollection<Gamme>? Gamme { get; set; }
    }
}
