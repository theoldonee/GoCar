using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    public class OperationsManager
    {
        public static Dictionary<string, List<Car>> carOperations = new Dictionary<string, List<Car>> { {"add", new List<Car>() }, { "delete", new List<Car>() } };
        public static Dictionary<string, List<Client>> clientOperations = new Dictionary<string, List<Client>> { { "add", new List<Client>() }, { "delete", new List<Client>() } };
        public static Dictionary<string, List<Rental>> rentalOperations = new Dictionary<string, List<Rental>> { { "add", new List<Rental>() }, { "delete", new List<Rental>() } };

        public static void Dump()
        {
            // opens connection to database
            using (var context = new CarRentalContext())
            {
                // Add operations
                foreach (var car in carOperations["add"])
                {
                    context.Car.Add(car);
                }

                foreach (var client in clientOperations["add"])
                {
                    context.Client.Add(client);
                }

                foreach (var rental in rentalOperations["add"])
                {
                    context.Rental.Add(rental);
                }

                // Delete operations
                foreach (var car in carOperations["delete"])
                {
                    context.Car.Remove(car);
                }

                foreach (var client in clientOperations["delete"])
                {
                    context.Client.Remove(client);
                }

                foreach (var rental in rentalOperations["delete"])
                {
                    context.Rental.Remove(rental);
                }


                context.SaveChanges();

            }
        }
    }
}
