using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    public class Client
    {
        public string ClientId { get; set; } // Primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        // Rental relationship
        public List<Rental> Rental { get; set; }

    }
}
