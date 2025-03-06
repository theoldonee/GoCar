using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    internal class Car
    {
        public string RegPlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
