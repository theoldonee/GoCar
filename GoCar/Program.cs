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

void AddToDatabase()
{
    //put added object into desired table
    Console.WriteLine("Choose a table to add to\n1: Car\n2: Client\n3: Rental");
    Console.WriteLine("Please enter a number:");
    string choice = Console.ReadLine();
    if (choice == "1")
    {
        Console.WriteLine("Car Id:");
        string carId = Console.ReadLine();
        // validate car Id
        // Use validator class

        Console.WriteLine("Make:");
        string make = Console.ReadLine();

        Console.WriteLine("Model:");
        string model = Console.ReadLine();

        Console.WriteLine("Fuel type:");
        string fuelType = Console.ReadLine();
        // validate fuel type

        Console.WriteLine("Type:");
        string type = Console.ReadLine();

        Console.WriteLine("Year:");
        int year = Int32.Parse(Console.ReadLine());

        var newCar = new Car
        {
            CarId = carId,
            Make = make,
            Model = model,
            FuelType = fuelType,
            Type = type,
            Year = year,
            Available = true
        };

        // add to data struct

        // update database
        using(var context = new CarRentalContex())
        {
            context.Car.Add(newCar);

            context.SaveChanges();

        }
    }else if(choice == "2"){

    }
    else
    {

    }
    //check if add is successfull

    // make same change and commit to database

    // check if input does not exist in db
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
