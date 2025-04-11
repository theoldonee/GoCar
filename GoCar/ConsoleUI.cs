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
