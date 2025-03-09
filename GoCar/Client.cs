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
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public int Email { get; set; }

        // Rental relationship
        public List<Rental> Rental { get; set; }

    }
}
