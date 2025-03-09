using GoCar;
using Microsoft.EntityFrameworkCore;

string gocarAscii = @"

         ░▒▓██████▓▒░ ░▒▓██████▓▒░        ░▒▓██████▓▒░ ░▒▓██████▓▒░░▒▓███████▓▒░  
        ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
        ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
        ░▒▓█▓▒▒▓███▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓████████▓▒░▒▓███████▓▒░  
        ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
        ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
         ░▒▓██████▓▒░ ░▒▓██████▓▒░        ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░  



";
Console.WriteLine(gocarAscii);

// Check if there is data present in the car database
bool databaseCheckResult = CheckCarDb();

// IF yes, ask user if they would like to load database  or use file
// IF no, Ask user if they would like to use default file or enter their own file

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


