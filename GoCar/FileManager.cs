using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    internal class FileManager
    {
        static string defaultPath = "../../../dataset/cars_data.csv";
        public static string alternatePath = "";
        static List<Car> carList = new List<Car>();
        static List<Car> carsToLoad = new List<Car>();
        static CarHashTable<string> carDbHashTable = new CarHashTable<string>();
        public static bool LoadFile(bool useDefault)
        {
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

            //check if file exist
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
                                Available = true
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
                ConsoleUI.DisplayDialog("Unsuccessful", "File does not exist", true, true);
                return false;
            }

            LoadDatabase();

            foreach(var car in carList)
            {
                Car carSearch = carDbHashTable.Search(car.CarId);
                // check if a car was gotten
                if (!(carSearch is Car))
                {
                    carsToLoad.Add(car);
                }
            }

            
            // opens connction to database
            using (var contex = new CarRentalContext())
            {
                
                foreach (var car in carsToLoad)
                {
                    contex.Car.Add(car);
                }

                contex.SaveChanges();
            }

            return true;
        }

        // loads database
        public static void LoadDatabase()
        {
            //var carStorage;
            using (var contex = new CarRentalContext())
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
