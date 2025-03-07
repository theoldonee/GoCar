using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    internal class Rental
    {
        public int RentalId { get; set; }
        public string CollectionDate { get; set; }
        public string ReturnDate { get; set; }

        //Car relationship
        public string CarId { get; set; }
        public Car Car { get; set; }

        //Client relationship
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
