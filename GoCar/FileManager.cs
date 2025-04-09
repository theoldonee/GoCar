using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    internal class FileManager
    {
        static string defaultPath = "../../../dataset/dummy.csv";
        public static string alternatePath = "";
        static List<Car> carList = new List<Car>();
        static List<Car> carsToLoad = new List<Car>();
        static CarHashTable<string> carDbHashTable = new CarHashTable<string>();
        public static bool LoadFile(bool useDefault)
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
                        // Add to a hashtable
                        try
                        {

                            Car car = new Car
                            {
                                CarId = values[0],
                                Make = values[1],
                                Model = values[2],
                                FuelType = values[3],
                                Type = values[4],
                                Year = Int32.Parse(values[5]),
                                Available = bool.Parse(values[6])
                            };

                            carList.Add(car);
                        }
                        catch
                        {

                        }


                    }

                }

            }
            else
            {
                Console.WriteLine("File does not exist");
                return false;
            }

            LoadDatabase();

            foreach(var car in carList)
            {
                Car carSearch = carDbHashTable.Search(car.CarId);
                if (!(carSearch is Car))
                {
                    carsToLoad.Add(car);
                }
            }

            using (var contex = new CarRentalContex())
            {
                
                foreach (var car in carsToLoad)
                {
                    contex.Car.Add(car);
                }

                contex.SaveChanges();
            }

            return true;
        }

        public static void LoadDatabase()
        {
            //var carStorage;
            using (var contex = new CarRentalContex())
            {
                var cars = contex.Car.ToList();
                foreach (var car in cars)
                {
                    carDbHashTable.Insert(car.CarId, car);
                }
            }
        }
    }
}
