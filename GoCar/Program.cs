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
// IF yes, ask user if they would like to load database  or use file
// IF no, Ask user if they would like to use default file or enter their own file

// DATA IN DATA STRUCT
// User can select an operation, selects a table, enter information

// Remove uses a unique ID
// Add requires all information
// Search can be don by the attributes of the class
