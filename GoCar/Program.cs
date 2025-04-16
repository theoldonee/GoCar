using System.Globalization;
using GoCar;

internal class Program
{
    // DATA IN DATA STRUCTS (car, rental, client);
    static CarHashTable<string> carHashTable = new CarHashTable<string>();
    static ClientHashTable<string> clientHashTable = new ClientHashTable<string>();
    static RentaltHashTable<string> rentalHashTable = new RentaltHashTable<string>();

    //MAIN METHOD
    public static void Main(string[] args)
    {
        // Create an instance of the UI, passing in the hash tables

        // Start the UI menu loop

        ConsoleUI.ShowMenuWithNavigation();
        // Check if there is data present in the car database
        bool databaseCheckResult = CheckCarDb();

        // IF yes, ask user if they would like to load database  or use file
        // check's database  check result
        if (databaseCheckResult)
        {
            string load = ConsoleUI.LoadDatabaseOrFileMenu();

            if (load == "1")
            {
                LoadDatabase();
            }
            else
            {
                LoadFile(databaseCheckResult);
            }

        }
        else // IF no, Ask user if they would like to use default file or enter their own file
        {

            LoadFile(databaseCheckResult);
        }


        // User can select an operation, selects a table, enter information
        string[] choiceArr = new string[2];
        bool execute = true;
        while (execute)
        {
            // get user's choice of database
            choiceArr[0] = ConsoleUI.SelectDatabaseOperation();

            choiceArr[1] = ConsoleUI.SelectDatabase();

            Execute(choiceArr);

            string choice = ConsoleUI.PerformOperation();
            Console.WriteLine("\n");

            if (choice != "1")
            {
                execute = false;
                ConsoleUI.DisplayDialog("Saving", "Saving to database", false, false);
                OperationsManager.Dump(); // dump to database

                ConsoleUI.DisplayDialog("Saved", "Your changes have been saved", false, true);
                ConsoleUI.Exit();
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
                    if (carHashTable.Count == 0)
                    {
                        ConsoleUI.DisplayDialog("Error", "Cannot add rental because car table is empty", true, true);
                    }
                    else
                    {
                        ConsoleUI.DisplayDialog("Error", "Cannot add rental because client table is empty", true, true);
                    }
                }

            }

        }
        else if (arr[0] == "2") // remove to database
        {
            // car
            if (arr[1] == "1")
            {
                RemoveCar();
            }
            else if (arr[1] == "2") // client
            {
                RemoveClient();
            }
            else if (arr[1] == "3") // rental
            {
                RemoveRental();
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
                SearchClient();
            }
            else if (arr[1] == "3") // rental
            {
                SearchRental();
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
        using (var contex = new CarRentalContext())
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
        ConsoleUI.DisplayDialog("Loading Database", "Loading.....", false, false);
        //var carStorage;
        using (var contex = new CarRentalContext())
        {
            var cars = contex.Car.ToList();
            foreach (var car in cars)
            {
                carHashTable.Insert(car.CarId, car);
            }
        }

        //var clientStorage;
        using (var contex = new CarRentalContext())
        {
            var clients = contex.Client.ToList();
            foreach (var client in clients)
            {
                clientHashTable.Insert(client.ClientId, client);
            }
        }

        //var rentalStorage;
        using (var contex = new CarRentalContext())
        {
            var rentals = contex.Rental.ToList();
            foreach (var rental in rentals)
            {
                rentalHashTable.Insert(rental.RentalId, rental);
            }
        }
    }

    // loads data from file to database
    public static void LoadFile(bool showMessage)
    {
        string load = ConsoleUI.SelectFileToLoad(showMessage);

        // load default file
        if (load == "1")
        {
            bool FileExist = FileManager.LoadFile(true);
            ConsoleUI.DisplayDialog("Loading Database", "Loading.....", false, false);
            if (!FileExist)
            {
                ConsoleUI.DisplayDialog("Unsuccessful", "Could not find file.", true, true);
            }
        }
        else // load custom file
        {
            Console.WriteLine("Enter a file path:");
            FileManager.alternatePath = Console.ReadLine();
            bool FileExist = FileManager.LoadFile(false);
            ConsoleUI.DisplayDialog("Loading Database", "Loading.....", false, false);
            if (!FileExist)
            {
                ConsoleUI.DisplayDialog("Unsuccessful", "Could not find file.", true, true);
            }
        }

        LoadDatabase();
    }

    // add car information
    public static bool AddCar()
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        // get plate number of car
        Console.WriteLine("\n");
        Console.WriteLine("Plate number:");
        string carId = GetCarID(true);

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

    // add client information
    public static bool AddClient()
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        // get client's first name of client
        Console.WriteLine("\n");
        Console.WriteLine("Enter client's first name: ");
        string firstName = Console.ReadLine();
        firstName = textInfo.ToTitleCase(firstName.ToLower());

        // get client's last name of client
        Console.WriteLine("\n");
        Console.WriteLine("Enter client's last name: ");
        string lastName = Console.ReadLine();
        lastName = textInfo.ToTitleCase(lastName.ToLower());

        // get client's phone number
        Console.WriteLine("\n");
        Console.WriteLine("Enter client's phone number: ");
        bool invalidPhoneNumber = true;
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
        string clientId = Validator.ClientValidator.GenerateId(firstName, lastName, clientHashTable);
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
        string rentalId = Validator.RentalValidator.GenerateId(rentalHashTable);
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
        string carId = GetCarID(false);
        // check if Id exist
        bool carIsValid = false;

        while (!carIsValid)
        {


            var car = carHashTable.Search(carId);
            if (car != null)
            {
                carIsValid = true;
            }
            else
            {
                Console.WriteLine("Please enter a car Id that exist within the database: ");
                carId = GetCarID(false);
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
            }
            else
            {

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

        rentalHashTable.Insert(newRental.RentalId, newRental);
        OperationsManager.rentalOperations["add"].Add(newRental);
        return true;
    }

    // get user input and check if CarId format is valid.
    public static string GetCarID(bool checkExistence)
    {
        Console.WriteLine("Enter Car ID: ");
        string carId = Console.ReadLine().ToUpper();

        // validate car Id
        bool idValidation = Validator.CarValidator.ValidateId(carId);

        bool carExist = false;
        if (checkExistence)
        {
            carExist = CarExist(carId);
        }


        // id entry validation check
        while (!idValidation || carExist)
        {
            if (carExist)
            {
                Console.WriteLine("Car plate number already exist. Please enter a new valid Car plate number.");
            }
            else
            {
                Console.WriteLine("Car plate number invalid, enter valid Car plate number.");
            }

            carId = Console.ReadLine().ToUpper();
            idValidation = Validator.CarValidator.ValidateId(carId);

            if (checkExistence)
            {
                carExist = CarExist(carId);
            }
            else
            {
                carExist = false;
            }
            Console.WriteLine("\n");
        }

        return carId;
    }

    // checks if a car exist
    public static bool CarExist(string carId)
    {
        bool result = false;
        Car car = carHashTable.Search(carId);
        if (car is Car)
        {
            result = true;
            Console.WriteLine("\nCar already exist in the database.");
        }

        return result;
    }

    // remove car
    public static void RemoveCar()
    {
        Console.WriteLine("\nEnter car's Id:");
        string carId = Console.ReadLine();

        Car car = carHashTable.Search(carId);
        if (car is Car)
        {
            carHashTable.Delete(carId);
            OperationsManager.carOperations["delete"].Add(car);
        }
        else
        {
            ConsoleUI.DisplayDialog("Unsuccessful", $"Could not find car {carId}.", true, true);
        }


    }

    // remove client
    public static void RemoveClient()
    {
        Console.WriteLine("\nEnter client's Id:");
        string clientId = Console.ReadLine();
        Client client = clientHashTable.Search(clientId);
        if (client is Client)
        {
            clientHashTable.Delete(clientId);
            OperationsManager.clientOperations["delete"].Add(client);
        }
        else
        {
            ConsoleUI.DisplayDialog("Unsuccessful", $"Could not find client {clientId}.", true, true);
        }
    }

    // remove rental
    public static void RemoveRental()
    {

        Console.WriteLine("\nEnter rental's Id:");
        try
        {

            string rentalId = Console.ReadLine();
            Rental rental = rentalHashTable.Search(rentalId);
            if (rental is Rental)
            {
                rentalHashTable.Delete(rentalId);
            }
            else
            {
                ConsoleUI.DisplayDialog("Unsuccessful", $"Could not find rental {rentalId}.", true, true);
            }
        }
        catch
        {
            ConsoleUI.DisplayDialog("Unsuccessful", "Entry invalid.", true, true);
        }

    }


    // search for car
    public static void SearchCar()
    {
        string searchBy = ConsoleUI.SearchCar();

        Console.WriteLine("\n");
        Console.WriteLine("Enter your search");
        string value = Console.ReadLine();

        // checks if the search is not by Id
        if (searchBy != "1")
        {
            var carList = carHashTable.SearchBy(value, searchBy);

            ConsoleUI.DisplayCars(carList);
        }
        else
        {

            Car car = carHashTable.Search(value);
            ConsoleUI.DisplayCar(car);
        }

        Console.WriteLine("\n");

    }

    // search for client
    public static void SearchClient()
    {
        string searchBy = ConsoleUI.SearchClient();

        Console.WriteLine("\n");
        Console.WriteLine("Enter your search");
        string value = Console.ReadLine();

        // checks if the search is not by Id
        if (searchBy != "1")
        {
            var clientList = clientHashTable.SearchBy(value, searchBy);

            ConsoleUI.DisplayClients(clientList);
        }
        else
        {
            try
            {
                Console.WriteLine("\n");
                var client = clientHashTable.Search(value);
                ConsoleUI.DisplayClient(client);
            }
            catch
            {
                Console.WriteLine("Cannot find client");
            }


        }


    }

    // search for rental
    public static void SearchRental()
    {

        string searchBy = ConsoleUI.SearchRental();

        Console.WriteLine("\n");
        Console.WriteLine("Enter your search");
        string value = Console.ReadLine();

        // checks if the search is not by Id
        if (searchBy != "1")
        {
            var rentalList = rentalHashTable.SearchBy(value, searchBy);

            ConsoleUI.DisplayRentals(rentalList);
        }
        else
        {
            try
            {
                Console.WriteLine("\n");
                var rental = rentalHashTable.Search(value);
                ConsoleUI.DisplayRental(rental);

            }
            catch
            {
                ConsoleUI.DrawHeader("Rental search result");
                ConsoleUI.DrawTableHead("rental");
                ConsoleUI.RedTextDisplay("No rental found.");
            }

        }

        Console.WriteLine("\n");

    }

}