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

            // CHANGED THE IF statement to TRY and CATCH blocks
            StreamReader reader = null;

            try
            {
                // read from file
                using (reader = new StreamReader(File.OpenRead(path)))
                {
                    //loop through lines in files
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        // Add to a list


                    }

                }

            }

            // change to catch block ??
            else
            {
                Console.WriteLine("File does not exist");
                return false;
            }

            // print message if file loading successful 
            return true;
        }
    }
}
