using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    public class Car
    {
        public string CarId { get; set; } // Primary key
        public string Make { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }

        //Rental relationship
        public List<Rental> Rental { get; set; }
    }
}
