using System.Globalization;
using GoCar;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

internal class Program
{

    // DATA IN DATA STRUCTS (car, rental, client);
    static CarHashTable<string> carHashTable = new CarHashTable<string>();
    static ClientHashTable<string> clientHashTable = new ClientHashTable<string>();
    static RentaltHashTable<int> rentalHashTable = new RentaltHashTable<int>();
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
        // check's database  check result
        if (databaseCheckResult)
        {
            Console.WriteLine("Database is not empty.\n1: Load from database.\n2: Load file.\n");
            Console.WriteLine("Please enter a number:");

            string load = Console.ReadLine();

            if (load == "1")
            {
                LoadDatabase();
            }

        }
        else // IF no, Ask user if they would like to use default file or enter their own file
        {
            Console.WriteLine("Database is empty.\n1: Load default file.\n2: Load another csv file\n");
            Console.WriteLine("Please enter a number:");
        }


        // User can select an operation, selects a table, enter information
        string[] choiceArr = new string[2];
        bool execute = true;
        while (execute)
        {
            // get user's choice of database
            Console.WriteLine("Choose an operation:\n1: Add to a database\n2: Remove from a database\n3: Search a database\n");
            Console.WriteLine("Please enter a number:");
            choiceArr[0] = Console.ReadLine();

            // get user's choice of database
            Console.WriteLine("\n");
            Console.WriteLine("Choose a table to perfom this operation\n1: Car\n2: Client\n3: Rental\n");
            Console.WriteLine("Please enter a number:");
            choiceArr[1] = Console.ReadLine();
            // Remove uses a unique ID
            // Add requires all information
            // Search can be don by the attributes of the class
        
            Execute(choiceArr);

            Console.WriteLine("Would you like to perform another operation?: \n1: Yes \n2: No");
            string choice = Console.ReadLine();
            Console.WriteLine("\n");

            if (choice != "1")
            {
                execute = false;
                OperationsManager.Dump();
                Console.WriteLine("\n");
                Console.WriteLine("Closing....");
            }
        }
        
    }

    // executes commands based on choices
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
                if (carHashTable.Count != 0 && clientHashTable.Count != 0)
                {
                    result = AddRental();
                }
                else
                {
                    Console.WriteLine("\n");
                    if (carHashTable.Count != 0)
                    {
                        Console.WriteLine("Cannot add rental. No car in database");
                    }
                    else
                    {
                        Console.WriteLine("Cannot add rental. No client in database");
                    }
                }
                
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
                SearchCar();
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

    // checks if database contains data
    public static bool CheckCarDb()
    {
        Console.WriteLine("Checking Database...");
        bool databaseNotEmpty = false;

        // open connection to database
        using (var contex = new CarRentalContex())
        {
            // get's all cars in database
            var cars = contex.Car.ToList();

            // checks if car list is empty
            if (cars.Count > 0)
            {
                databaseNotEmpty = true;
            }
        }

        // checks if car database is empty
        if (databaseNotEmpty)
        {
            return true;
        }
        return false;
    }

    // loads data from database
    public static void LoadDatabase()
    {
        //var carStorage;
        using(var contex = new CarRentalContex())
        {
            var cars = contex.Car.ToList();
            foreach( var car in cars)
            {
                carHashTable.Insert(car.CarId, car);
            }
        }

        //var clientStorage;
        using (var contex = new CarRentalContex())
        {
            var clients = contex.Client.ToList();
            foreach (var client in clients)
            {
                clientHashTable.Insert(client.ClientId, client);
            }
        }

        //var rentalStorage;
        using (var contex = new CarRentalContex())
        {
            var rentals = contex.Rental.ToList();
            foreach (var rental in rentals)
            {
                rentalHashTable.Insert(rental.RentalId, rental);
            }
        }
    }

    // add car
    public static bool AddCar()
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        // get plate number of car
        Console.WriteLine("\n");
        Console.WriteLine("Plate number:");
        string carId = GetCarID();

        // get make of car
        Console.WriteLine("\n");
        Console.WriteLine("Make:");
        string make = Console.ReadLine();

        // get model of car
        Console.WriteLine("\n");
        Console.WriteLine("Model:");
        string model = Console.ReadLine();

        // get car's fuel type
        Console.WriteLine("\n");
        Console.WriteLine("Fuel type:");
        string fuelType = Console.ReadLine();

        // convert fuel type to title case
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

        // gets the car type
        Console.WriteLine("\n");
        Console.WriteLine("Type:");
        string type = Console.ReadLine();

        // get year the car was made
        Console.WriteLine("\n");
        Console.WriteLine("Year:");
        bool invalidYear = true;
        int year = 0;

        //year check
        while (invalidYear)
        {
            try
            {
                //get year
                year = int.Parse(Console.ReadLine());

                // year validation
                bool yearValidation = Validator.CarValidator.ValidateYear(year);

                // check if year is valid
                if (yearValidation)
                {
                    invalidYear = false;
                }
                else
                {
                    Console.WriteLine("Year entry invalid");
                    Console.WriteLine("Please enter valid year:");
                }
            }
            catch
            {
                Console.WriteLine("Year entry invalid: Year cannot be a string.\nPlease enter valid year");
            }
            Console.WriteLine("\n");
        }

        // create new car
        Car newCar = new Car
        {
            CarId = carId,
            Make = make,
            Model = model,
            FuelType = fuelType,
            Type = type,
            Year = year,
            Available = true
        };

        //// add to data struct
        carHashTable.Insert(newCar.CarId, newCar);
        OperationsManager.carOperations["add"].Add(newCar);

        return true;
    }

    // add client 
    public static bool AddClient()
    {
        // get client's first name of client
        Console.WriteLine("\n");
        Console.WriteLine("Enter client's first name: ");
        string firstName = Console.ReadLine();

        // get client's last name of client
        Console.WriteLine("\n");
        Console.WriteLine("Enter client's last name: ");
        string lastName = Console.ReadLine();

        // get client's phone number
        Console.WriteLine("\n");
        Console.WriteLine("Enter client's phone number: ");
        bool invalidPhoneNumber= true;
        int phoneNumber = 0;

        // phone number check
        while (invalidPhoneNumber)
        {
            try
            {
                //convert phone number to int
                phoneNumber = int.Parse(Console.ReadLine());

                // validate phone number
                bool phoneNumberValidation = Validator.ClientValidator.ValidatePhoneNumber(phoneNumber);

                // check if phone number is valid
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

        // get client's email
        Console.WriteLine("Enter client's email: ");
        string email = "";
        bool emailValidation = false;
        while (!emailValidation)
        {
            // get email
            email = Console.ReadLine();

            // validate email
            emailValidation = Validator.ClientValidator.ValidateEmail(email);
            Console.WriteLine("\n");
        }

        //generate client Id
        Console.WriteLine("Enter client's Id: ");
        string clientId = Validator.ClientValidator.GenerateId(firstName, lastName);
        Console.WriteLine($"Client id is: {clientId}");

        // create new client
        Client newClient = new Client
        {
            ClientId = clientId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email
        };

        //Add too data structure
        clientHashTable.Insert(newClient.ClientId, newClient);
        OperationsManager.clientOperations["add"].Add(newClient);

        Console.WriteLine("\n");
        return true;
    }

    // add rental information
    public static bool AddRental()
    {
        // rental id
        // generated
        Console.WriteLine("\n");
        Console.WriteLine("Generating rental Id....");
        int rentalId = Validator.RentalValidator.GenerateId();
        Console.WriteLine($"Rental Id is {rentalId}");

        // collection date
        // generated
        string collectionDate = Validator.RentalValidator.GetDate();

        // return date
        Console.WriteLine("\n");
        Console.WriteLine("Enter the return date in the format 'dd-MM-yyyy': ");
        string returnDate = Console.ReadLine();
        bool invalidDate = true;
        while (invalidDate)
        {
            // return date validation
            if (Validator.RentalValidator.ValidateDate(returnDate))
            {
                invalidDate = false;
            }
            else
            {
                Console.WriteLine("Date invalid: Please enter a valid return date in the format 'dd-MM-yyyy': ");
                returnDate = Console.ReadLine();
            }
            Console.WriteLine("\n");
        }

        // carId
        string carId = GetCarID();
        // check if Id exist
        bool carIsValid = false;

        while(!carIsValid){

            
            var car = carHashTable.Search(carId);
            if (car != null)
            {
                carIsValid = true;
            }else{
                Console.WriteLine("Please enter a car Id that exist within the database: ");
                carId = GetCarID();
                Console.WriteLine("\n");
            }
        }
        

        //// client ID
        Console.WriteLine("Enter client's Id: ");
        string clientId = Console.ReadLine();
        bool clientIsValid = false;

        while (!clientIsValid)
        {

            var client = clientHashTable.Search(clientId);
            if (client != null)
            {
                clientIsValid = true;
            }else{

                Console.WriteLine("Please enter a client Id that exist within the database: ");
                clientId = Console.ReadLine();
                Console.WriteLine("\n");
            }
        }

        Rental newRental = new Rental
        {
            RentalId = rentalId,
            CollectionDate = collectionDate,
            ReturnDate = returnDate,
            CarId = carId,
            ClientId = clientId

        };

        OperationsManager.rentalOperations["add"].Add(newRental);
        return true;
    }

    // get user input and check if CarId format is valid.
    public static string GetCarID()
    {
        Console.WriteLine("Enter Car ID: ");
        string carId = Console.ReadLine().ToUpper();

        // validate car Id
        bool idValidation = Validator.CarValidator.ValidateId(carId);

        // id entry validation check
        while (!idValidation)
        {
            Console.WriteLine("Car plate number invalid, enter valid Car plate number.");
            carId = Console.ReadLine().ToUpper();
            idValidation = Validator.CarValidator.ValidateId(carId);
            Console.WriteLine("\n");
        }

        return carId;
    }

    // removecar
    public static bool RemoveCar()
    {
        string carId = GetCarID();
        carHashTable.Delete(carId);
        return true;
    }

    //search car
    public static void SearchCar()
    {
        Console.WriteLine("\n");
        // ask user what to search by 
        Console.WriteLine("What would you like to search by?");
        Console.WriteLine("1: Id");
        Console.WriteLine("2: Make");
        Console.WriteLine("3: Fuel type");
        Console.WriteLine("4: Type");
        Console.WriteLine("5: Year");
        Console.WriteLine("6: Availability");
        string searchBy = Console.ReadLine();

        Console.WriteLine("\n");
        Console.WriteLine("Enter your search");
        string value = Console.ReadLine();

        // checks if the search is not by Id
        if (searchBy != "1")
        {
            List<Car> carList = carHashTable.SearchBy(value, searchBy);

            // iterates over carList
            foreach(var car in carList)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"{car.CarId}, {car.Make}, {car.Model}, {car.FuelType}, {car.Type}, {car.Available}");
            }
        }
        else
        {
            Console.WriteLine("\n");
            Car car = carHashTable.Search(value);
            Console.WriteLine($"{car.CarId}, {car.Make}, {car.Model}, {car.FuelType}, {car.Type}, {car.Available}");
        }

        Console.WriteLine("\n");

    }

}