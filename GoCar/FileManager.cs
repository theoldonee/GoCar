using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(_filePath);

            string path;
            //check path to use
            if (useDefault)
            {
                path = defaultPath;
            }
            else
            {
                path = alternatePath;
            }

            StreamReader reader = null;

            //Check if file exist
            //check if file path is a csv
            if (File.Exists(path))
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
            else
            {
                Console.WriteLine("File does not exist");
                return false;
            }
            return true;
        }
    }
}
