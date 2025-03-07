using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    internal class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public int Email { get; set; }

        // Rental relationship
        List<Rental> Rental { get; set; }

    }
}
