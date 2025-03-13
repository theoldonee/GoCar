using System.Globalization;
using GoCar;
using Microsoft.EntityFrameworkCore;

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
AddToDatabase();

// Remove uses a unique ID
// Add requires all information
// Search can be don by the attributes of the class


bool CheckCarDb()
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

void LoadDatabase()
{
    //var carStorage;
    //var clientStorage;
    //var rentalStorage;
}

bool AddToDatabase()
{
    //put added object into desired table
    Console.WriteLine("Choose a table to add to\n1: Car\n2: Client\n3: Rental\n");
    Console.WriteLine("Please enter a number:");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        bool result = AddCar();
        if (result)
        {
            Console.WriteLine("Car Sucessfully Added");
        }
        else
        {
            return false;
        }
    }
    else if(choice == "2"){
        bool result = AddClient();
        if (result)
        {
            Console.WriteLine("Client Sucessfully Added");
        }
        else
        {
            return false;
        }
    }
    else
    {

    }
    //check if add is successfull

    // make same change and commit to database

    // check if input does not exist in db
    return false;
}

void RemoveFromDatabase()
{
    // check if input exist in db

    //check if removal is successfull

    // make same change and commit to database


}

void SearchDatabase()
{

}

// Add Car
bool AddCar()
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
            year = Int32.Parse(Console.ReadLine());
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

// ADD CLIENT HAS ISSUES ROLL BACK MIGRATION
// ADD FIRST NAME AND LAST NAME
bool AddClient() { 

    // First Name, Last Name
    Console.WriteLine("Enter client's name: ");
    string firstName = Console.ReadLine();

    Console.WriteLine("Enter client's name: ");
    string lastName = Console.ReadLine();

    Console.WriteLine("Enter client's phone number: ");
    int phoneNumber = Int32.Parse(Console.ReadLine());

    Console.WriteLine("Enter client's email: ");
    string email = Console.ReadLine();

    //generate client Id
    Console.WriteLine("Enter client's email: ");
    string clientId = Console.ReadLine();

    Client newClient = new Client
    {
        ClientId = clientId,
        FirstName = firstName,
        LastName = lastName,
        PhoneNumber = phoneNumber,
        // email is int in class
        Email = email
    };

    //Add too data structure
    //Add to database
    return true;
}