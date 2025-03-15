using System.Globalization;
using GoCar;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

internal class Program
{
    public static void Main(string[] args)
    {
        string gocarAscii = @"
        
 ░▒▓██████▓▒░ ░▒▓██████▓▒░        ░▒▓██████▓▒░ ░▒▓██████▓▒░░▒▓███████▓▒░       ░▒▓█▓▒░   ░▒▓████████▓▒░▒▓███████▓▒░  
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒▒▓███▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓████████▓▒░▒▓███████▓▒░       ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
 ░▒▓██████▓▒░ ░▒▓██████▓▒░        ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓████████▓▒░▒▓█▓▒░   ░▒▓███████▓▒░  
                     
";
        Console.WriteLine(gocarAscii);

        // Check if there is data present in the car database
        bool databaseCheckResult = CheckCarDb();

        // IF yes, ask user if they would like to load database  or use file
        if (databaseCheckResult)
        {
            Console.WriteLine("Database is not empty.\n1: Load from database.\n2: Load file.\n");
            Console.WriteLine("Please enter a number:");

        }
        else // IF no, Ask user if they would like to use default file or enter their own file
        {
            Console.WriteLine("Database is empty.\n1: Load default file.\n2: Load another csv file\n");
            Console.WriteLine("Please enter a number:");
        }



        // DATA IN DATA STRUCTS (car, rental, client);
        List<Car> CarList;
        List<Client> ClientList;
        List<Rental> RentalList;
        // User can select an operation, selects a table, enter information
        string[] choiceArr = new string[2];

        Console.WriteLine("Choose an operation:\n1: Add to a database\n2: Remove from a database\n3: Search a database\n");
        Console.WriteLine("Please enter a number:");
        choiceArr[0] = Console.ReadLine();

        Console.WriteLine("Choose a table to perfom this operation\n1: Car\n2: Client\n3: Rental\n");
        Console.WriteLine("Please enter a number:");
        choiceArr[1] = Console.ReadLine();
        // Remove uses a unique ID
        // Add requires all information
        // Search can be don by the attributes of the class
        Execute(choiceArr);

        
    }

    public static bool Execute(string[] arr)
    {
        bool result = false;
        // add to database
        if (arr[0] == "1")
        {
            // car
            if (arr[1] == "1")
            {
                result = AddCar();
            }
            else if (arr[1] == "2") // client
            {
                result = AddClient();
            }
            else if (arr[1] == "3") // rental
            {
                result = AddClient();
            }

        }
        else if (arr[0] == "2") // remove to database
        {
            // car
            if (arr[1] == "1")
            {
                result = AddCar();
            }
            else if (arr[1] == "2") // client
            {
                result = AddClient();
            }
            else if (arr[1] == "3") // rental
            {
                result = AddClient();
            }

        }
        else if (arr[0] == "3") // search to database
        {
            // car
            if (arr[1] == "1")
            {
                result = AddCar();
            }
            else if (arr[1] == "2") // client
            {
                result = AddClient();
            }
            else if (arr[1] == "3") // rental
            {
                result = AddClient();
            }
        }

        return result;
    }

    public static bool CheckCarDb()
    {
        Console.WriteLine("Checking Database...");
        bool databaseNotEmpty = false;
        using (var contex = new CarRentalContex())
        {
            var cars = contex.Car.ToList();

            if (cars.Count > 0)
            {
                databaseNotEmpty = true;
            }
        }

        if (databaseNotEmpty)
        {
            return true;
        }
        return false;
    }

    public static void LoadDatabase()
    {
        //var carStorage;
        //var clientStorage;
        //var rentalStorage;
    }


    // add car
    public static bool AddCar()
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        Console.WriteLine("\n");
        // get plate number of car
        Console.WriteLine("Plate number:");
        string carId = Console.ReadLine().ToUpper();

        // validate car Id
        bool idValidation = Validator.CarValidator.ValidateId(carId);
        while (!idValidation)
        {
            Console.WriteLine("Car plate number invalid, enter valid Car plate number.");
            carId = Console.ReadLine().ToUpper();
            idValidation = Validator.CarValidator.ValidateId(carId);
            Console.WriteLine("\n");
        }

        Console.WriteLine("\n");
        // get make of car
        Console.WriteLine("Make:");
        string make = Console.ReadLine();

        Console.WriteLine("\n");
        // get model of car
        Console.WriteLine("Model:");
        string model = Console.ReadLine();

        Console.WriteLine("\n");
        Console.WriteLine("Fuel type:");
        string fuelType = Console.ReadLine();
        fuelType = textInfo.ToTitleCase(fuelType.ToLower());

        Console.WriteLine("\n");
        // validate fuel type
        bool fuelValidation = Validator.CarValidator.ValidateFuelType(fuelType);
        while (!fuelValidation)
        {
            Console.WriteLine("Fuel type invalid, enter valid fuel type");
            fuelType = Console.ReadLine();
            // validate fuel type
            fuelType = textInfo.ToTitleCase(fuelType.ToLower());
            fuelValidation = Validator.CarValidator.ValidateFuelType(fuelType);
            Console.WriteLine("\n");
        }

        Console.WriteLine("\n");
        Console.WriteLine("Type:");
        string type = Console.ReadLine();

        Console.WriteLine("\n");
        Console.WriteLine("Year:");
        //Type check
        bool invalidYear = true;
        int year;
        while (invalidYear)
        {
            try
            {
                year = int.Parse(Console.ReadLine());
                bool yearValidation = Validator.CarValidator.ValidateYear(year);
                if (yearValidation)
                {
                    invalidYear = false;
                }
                else
                {
                    Console.WriteLine("Year entry invalid:Year cannot exceed current year");
                    Console.WriteLine("Please enter valid year:");
                }
            }
            catch
            {
                Console.WriteLine("Year entry invalid. Please enter valid year");
            }
            Console.WriteLine("\n");
        }

        //var newCar = new Car
        //{
        //    CarId = carId,
        //    Make = make,
        //    Model = model,
        //    FuelType = fuelType,
        //    Type = type,
        //    Year = year,
        //    Available = true
        //};

        //// add to data struct

        //// update database
        //using (var context = new CarRentalContex())
        //{
        //    context.Car.Add(newCar);

        //    context.SaveChanges();

        //}

        return true;
    }

    // add client 
    public static bool AddClient()
    {

        Console.WriteLine("Enter client's name: ");
        string firstName = Console.ReadLine();

        Console.WriteLine("\n");
        Console.WriteLine("Enter client's name: ");
        string lastName = Console.ReadLine();

        Console.WriteLine("\n");
        Console.WriteLine("Enter client's phone number: ");
        bool invalidPhoneNumber= true;
        int phoneNumber = 0;
        while (invalidPhoneNumber)
        {
            try
            {
                //convert phone number to int
                phoneNumber = int.Parse(Console.ReadLine());

                // validate phone number
                bool phoneNumberValidation = Validator.ClientValidator.ValidatePhoneNumber(phoneNumber);
                if (phoneNumberValidation)
                {
                    invalidPhoneNumber = false;
                }
                else
                {
                    Console.WriteLine("Phone number entry invalid");
                    Console.WriteLine("Please enter valid mauritian phone number:");
                }
            }
            catch
            {
                Console.WriteLine("Phone number entry invalid. Phone numbers cannot contain letters");
                Console.WriteLine("Please enter valid mauritian phone number:");
            }
            Console.WriteLine("\n");
        }

        Console.WriteLine("Enter client's email: ");
        string email = "";
        bool emailValidation = false;
        while (!emailValidation)
        {
            email = Console.ReadLine();
            // validate email
            emailValidation = Validator.ClientValidator.ValidateEmail(email);
            Console.WriteLine("\n");
        }
        //generate client Id
        Console.WriteLine("Enter client's Id: ");
        string clientId = Console.ReadLine();

        Client newClient = new Client
        {
            ClientId = clientId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email
        };

        //Add too data structure
        //Add to database
        return true;
    }
}