using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Clients
{
    public class DtoClient : BasicElement
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CompagnieName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}
