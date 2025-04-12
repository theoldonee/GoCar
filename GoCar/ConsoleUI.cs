using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace GoCar
{
    internal class ConsoleUI
    {

        private static string gocarAscii = @"
        
 ░▒▓██████▓▒░ ░▒▓██████▓▒░        ░▒▓██████▓▒░ ░▒▓██████▓▒░░▒▓███████▓▒░       ░▒▓█▓▒░   ░▒▓████████▓▒░▒▓███████▓▒░  
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒▒▓███▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓████████▓▒░▒▓███████▓▒░       ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░   ░▒▓█▓▒░░▒▓█▓▒░ 
 ░▒▓██████▓▒░ ░▒▓██████▓▒░        ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓████████▓▒░▒▓█▓▒░   ░▒▓███████▓▒░  
                     
";
        // Displays the main menu with arrow key navigation
        public static string ShowMenuWithNavigation()
        {
            // Menu options displayed to the user
            string[] options = {
                "1. Database",
                "2. About Us",
                "3. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            DrawAscii(false);
            Console.ForegroundColor = ConsoleColor.Green; // Sets title color to green
            TypeEffect("Welcome to GoCar - Car Rental System!"); // Animated intro
            Console.ResetColor();
            TypeEffect("Use arrow keys to navigate and press Enter to select.\n");
            Thread.Sleep(2000);


            while (true) // Menu loop
            {
                DrawAscii(true);
                SelectedIndex(selectedIndex, options);

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: return "1"; break;
                        case 1: AboutUs(); break;
                        case 2: Exit(); break;
                    }
                }
            }

        }

        public static string LoadDatabaseOrFileMenu()
        {
            // Menu options displayed to the user
            string[] options = {
                "1. Load from database",
                "2. Load from file",
                "3. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            while (true) // Menu loop
            {
                DrawAscii(true);
                SelectedIndex(selectedIndex, options);

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: return "1"; break;
                        case 1: return "2"; break;
                        case 2: Exit(); break;
                    }
                }
            }
        }

        public static string SelectDatabaseOperation() 
        { 
            // Menu options displayed to the user
            string[] options = {
                "1. Add to a database",
                "2. Remove from a database",
                "3. Search a database",
                "4. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            while (true) // Menu loop
            {
                DrawAscii(true);
                SelectedIndex(selectedIndex, options);

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: return "1"; break;
                        case 1: return "2"; break;
                        case 2: return "3"; break;
                        case 3: Exit(); break;
                    }
                }
            }
        }

        public static string SelectDatabase()
        {
            // Menu options displayed to the user
            string[] options = {
                "1. Car",
                "2. Client",
                "3. Rental",
                "4. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            while (true) // Menu loop
            {
                DrawAscii(true);
                SelectedIndex(selectedIndex, options);

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: return "1"; break;
                        case 1: return "2"; break;
                        case 2: return "3"; break;
                        case 3: Exit(); break;
                    }
                }
            }
        }

        public static string SearchCar()
        {
            // Menu options displayed to the user
            string[] options = {
                "1. Id",
                "2. Make",
                "3. Fuel type",
                "4. Type",
                "5. Year",
                "6. Availability",
                "7. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            while (true) // Menu loop
            {
                DrawAscii(true);
                Console.WriteLine("What would you like to search by?");
                SelectedIndex(selectedIndex, options);

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: return "1"; break;
                        case 1: return "2"; break;
                        case 2: return "3"; break;
                        case 3: return "4"; break;
                        case 4: return "5"; break;
                        case 5: return "6"; break;
                        case 6: return "7"; break;
                        case 7: return "8"; break;
                        case 8: Exit(); break;
                    }
                }
            }
        }

        public static string SearchClient()
        {
            // Menu options displayed to the user
            string[] options = {
                "1. Id",
                "2. Initials",
                "3. First name",
                "4. Last name",
                "5. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            while (true) // Menu loop
            {
                DrawAscii(true);
                Console.WriteLine("What would you like to search by?");
                SelectedIndex(selectedIndex, options);

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: return "1"; break;
                        case 1: return "2"; break;
                        case 2: return "3"; break;
                        case 3: return "4"; break;
                        case 5: Exit(); break;
                    }
                }
            }
        }

        public static void DisplayCars(IEnumerable<Car> carList)
        {
            DrawHeader("Car search result");
            DrawTableHead("car"); // Draws the table header

            //if carList is empty
            if (carList.Count() == 0)
                RedTextDisplay("No car(s) found.");
            else
                foreach (Car car in carList) {
                    Console.WriteLine($"{car.CarId, -10}| {car.Make,-12}| {car.Model,-12}| {car.FuelType,-10}| {car.Type,-15}| {car.Year,-10}| {car.Available,-10}");
                }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public static void DisplayCar(Car car)
        {
            DrawHeader("Car search result");
            DrawTableHead("car"); // Draws the table header
            //
            //if car is a car object
            if (!(car is Car))
            {
                RedTextDisplay("No car was found.");
            }
            else
            {
                // Print the car details
                Console.WriteLine($"{car.CarId,-10}| {car.Make,-12}| {car.Model,-12}| {car.FuelType,-10}| {car.Type,-15}| {car.Year,-10}| {car.Available,-10}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }


        public static void RedTextDisplay(string message)
        {
            Console.WriteLine($"\n\u001b[31m{message}\u001b[0m");
        }

        public static void DrawHeader(string message)
        {
            Console.Clear();
            DrawAscii(true);
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeEffect($"\n{message}");
            Console.ResetColor();
        }

        public static void DrawTableHead(string table)
        {
            if(table == "car")
            {
                // Print the table header with column names
                // -10, -12, -12, -6 are alignment specifiers
                // Negative means left-aligned; the number sets the width of the column
                Console.WriteLine($"\n{"Car ID",-10}| {"Make",-12}| {"Model",-12}| {"Fuel Type",-10}| {"Type",-15}| {"Year",-10}| {"Availability",-10}");

                // Print a horizontal line for visual separation under the headers
                // This draws 45 dashes to match the width of the table
                Console.WriteLine(new string('-', 100));
            }
            else if (table == "client")
            {
                Console.WriteLine($"{"ClientId",-10}| {"FirstName",-12}| {"LastName",-12}| {"PhoneNumber",-10}| {"Email",-15}");
            }
            else if (table == "rental")
            {
                
            }
            
        }

        public static void Exit(){
            Console.ForegroundColor = ConsoleColor.Red; //text color to Red
            TypeEffect("\nYou have exited. Thank you for using GoCar!");
            Console.ResetColor();
            Environment.Exit(0); // Exit application
        }

        public static void SelectedIndex(int selectedIndex, string[] options)
        {
            // Loop through each menu option
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {   // Highlight selected option with a diff colour 
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else Console.ResetColor();

                Console.WriteLine(options[i]);
                Console.ResetColor(); // Reset color after each option
            }
        }
        public static void DrawAscii(bool show)
        {
            Console.Clear(); // Clears the screen

            Console.WriteLine(gocarAscii);
            if (show) Console.WriteLine("Navigate using Arrow keys. \nPress Enter to select.\n");
        }
        public static void AboutUs()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            TypeEffect("About Us");
            Console.ResetColor();

            Console.WriteLine("\nGoCar is a reliable and customer-centric car rental service,");
            Console.WriteLine("committed to making transportation accessible and hassle-free.");
            Console.WriteLine("Whether for travel, commuting, or business, GoCar offers a wide");
            Console.WriteLine("fleet of vehicles to meet your needs. Enjoy transparent pricing,");
            Console.WriteLine("a smooth booking experience, and great service with GoCar.");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        // Prints text with typing effect - cool stuff!! 
        public static void TypeEffect(string text)
        {
            //Simulates typing animation by printing one character at a time.
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20); // 20 ms delay between characters
            }
            Console.WriteLine();
        }

        // Shows a dialog box with a title and message
        public static void DisplayDialog(string title, string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"==== {title} ====");
            Console.ResetColor();
            Console.WriteLine(message);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
