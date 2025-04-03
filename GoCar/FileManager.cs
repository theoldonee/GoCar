using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internal;

namespace GoCar
{
    // Handles file operations for loading data into hash tables.
    internal class FileManager
    {
        // Default file path for the dataset
        static string defaultPath = "../../../dataset/dummy.csv";

        // Alternate file path that can be set dynamically
        public static string alternatePath = "";

        //  Loads data from a specified CSV file into the provided hash tables
        public static bool LoadFile(string filePath, CarHashTable<string> carTable, ClientHashTable<string> clientTable, RentaltHashTable<string> rentalTable)
        {
            // Determine which file path to use (default or provided)
            string path = string.IsNullOrWhiteSpace(filePath) ? defaultPath : filePath;

            // Check if the file exists before attempting to read it
            if (!File.Exists(path))
            {
                Console.WriteLine($"Error: File '{path}' does not exist.");
                return false; // Return false as the file could not be found
            }

            try
            {
                // Open the file using StreamReader
                using (StreamReader reader = new StreamReader(File.OpenRead(path)))
                {
                    // Read the file line by line until the end is reached
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine(); // Read a single line from the file

                        // Ensure the line is not empty or null before processing
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            // Split the line into an array using commas as delimiters
                            var values = line.Split(',');

                            // Determine the type of record (Car, Client, or Rental) based on the first column
                            switch (values[0].Trim().ToLower()) // Convert to lowercase for consistency
                            {
                                case "car":
                                    // Create a new Car object and insert it into the carTable
                                    Car car = new Car(values[1], values[2], values[3], int.Parse(values[4]), bool.Parse(values[5]));
                                    carTable.Insert(car.Id, car);
                                    break;

                                case "client":
                                    // Create a new Client object and insert it into the clientTable
                                    Client client = new Client(values[1], values[2], values[3]);
                                    clientTable.Insert(client.Id, client);
                                    break;

                                case "rental":
                                    // Create a new Rental object and insert it into the rentalTable
                                    Rental rental = new Rental(values[1], values[2], values[3], values[4]);
                                    rentalTable.Insert(rental.RentalId, rental);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while reading the file
                Console.WriteLine($"Error reading file: {ex.Message}");
                return false; // Return false to indicate failure
            }

            
            return true;
        }
    }
}
