using System;
using System.Collections.Generic;
using System.Threading;


namespace GoCar
{
    public class ConsoleUI
    {

        // Displays the main menu with arrow key navigation
        public void ShowMenuWithNavigation()
        {
            // Menu options displayed to the user
            string[] options = {
                "1. Add Car",
                "2. Search Car",
                "3. View All Cars",
                "4. Edit Car",
                "5. Delete Car",
                "6. About Us",
                "7. Exit"
            };

            // Defines the list of menu options displayed to the user.
            int selectedIndex = 0; // Tracks currently highlighted menu item

            Console.Clear(); // Clears the screen
            Console.ForegroundColor = ConsoleColor.Green; // Sets title color to green
            TypeEffect("Welcome to GoCar - Car Rental System!"); // Animated intro
            Console.ResetColor();
            TypeEffect("Use arrow keys to navigate and press Enter to select.\n");

            while (true) // Menu loop
            {
                Console.Clear(); // Refresh screen
                Console.WriteLine("Navigate using Arrow keys. \nPress Enter to select.\n");

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

                var key = Console.ReadKey(true); //Reads a key press from user without displaying it on screen

                // Navigate down or up the list based on arrow key presses
                if (key.Key == ConsoleKey.DownArrow) selectedIndex = (selectedIndex + 1) % options.Length;
                else if (key.Key == ConsoleKey.UpArrow) selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Call appropriate method based on selection
                    switch (selectedIndex)
                    {
                        case 0: AddCar(); break;
                        case 1: SearchCar(); break;
                        case 2: DisplayAllCars(); break;
                        case 3: EditCar(); break;
                        case 4: DeleteCar(); break;
                        case 5: AboutUs(); break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.Red; //text color to Red
                            TypeEffect("\nYou have exited. Thank you for using GoCar!");
                            Console.ResetColor();
                            Environment.Exit(0); // Exit application
                            break;
                    }
                }
            }
        } // end of Method: ShowMenuWithNavigation



    } // end of Class:ConsoleUI

}
