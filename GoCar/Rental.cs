using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    internal class Rental
    {
        int RentalId { get; set; }
        string CollectionDate { get; set; }
        string ReturnDate { get; set; }

        //Car relationship
        string CarPlateNumber { get; set; }
        public Car Car { get; set; }

        //Client relationship
        int ClientId { get; set; }
        Client Client { get; set; }
    }
}
