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



// DATA IN DATA STRUCT
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

