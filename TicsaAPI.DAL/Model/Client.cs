using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicsaAPI.DAL.Model
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string CompanieName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
    }
}
