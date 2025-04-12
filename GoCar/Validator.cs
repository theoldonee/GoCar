using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCar
{
    public class Validator
    {
        // Validates car info
        public class CarValidator{
            // Validates car id
            public static bool ValidateId(string id)
            {
                string[] splitId = id.Split('-');
                bool result = false;

                // checks if Id has -
                if (splitId.Length == 2)
                {
                    // checks if both halves of contain 4 characters
                    if (splitId[0].Length == 4 && splitId[1].Length == 4)
                    {
                        //Debug.Assert(!(Checker("Y798") == "lnnn"), "True");
                        //Debug.Assert(!(Checker("YY98") == "llnn"), "True");
                        //Debug.Assert(!(Checker("YBB8") == "llln"), "True");
                        //Debug.Assert(!(Checker("9798") == "nnnn"), "True");

                        // checks if Id has the correct format
                        if (Checker(splitId[0]) == "lnnn" && Checker(splitId[1]) == "llnn")
                        {
                            result = true;
                        }
                        else
                        {
                            Console.WriteLine("Id is not in correct format");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Both halves of the licence plate number are to contain 4 digits");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid licence plate number: No '-'");
                }

                return result;

            }

            // Checks array to determine Id format.
            private static string Checker(string stringArray)
            {
                string result = "";

                // iterates over characters in the string
                foreach (char items in stringArray)
                {
                    // tries to convert string to integer
                    try
                    {
                        int number = Int32.Parse(items.ToString());
                        result += "n";
                    }
                    catch
                    {
                        result += "l";
                    }
                }

                return result;
            }

            // Validate fuel type
            public static bool ValidateFuelType(string fuelType)
            {
                // checks the fueltype
                if (fuelType == "Diesel" || fuelType == "Petrol" || fuelType == "Electric" || fuelType == "Hybrid")
                {
                    return true;
                }

                return false;
            }

            // validate year
            public static bool ValidateYear(int year)
            {
                bool result;
                int currentYear = DateTime.Now.Year;

                // checks if year is above 1999 and below current year
                if (year > 1999 && year <= currentYear)
                {
                    result = true;
                }
                else
                {
                    // checks if year is greater than current year
                    if (year > currentYear)
                    {
                        Console.WriteLine("Year caannot be greater than current year");
                    }
                    else
                    {
                        Console.WriteLine("Year caannot be less than 2000");
                    }
                    result = false;
                }
                return result;
            }
        }

        // Validates client info
        public class ClientValidator
        {
            // validates client's phone number
            public static bool ValidatePhoneNumber(int phoneNumber)
            {
                string phoneNo = phoneNumber.ToString();
                
                // checks the length of the phone number
                if (phoneNo.Length != 8)
                {
                    return false;
                }

                return true;
            }

            // validates email
            public static bool ValidateEmail(string email)
            {
                string[] splitByAt = email.Split("@");

                // check if email contains "@"
                if (splitByAt.Length != 2)
                {
                    Console.WriteLine("Email invalid: no '@' sign");
                    return false;

                }else if (splitByAt[0] == "") // check if email starts with "@"
                {
                    Console.WriteLine("Email invalid: Email cannot atart with '@' sign");
                    return false;
                }
                else
                {
                    string[] splitByDot = splitByAt[1].Split(".");

                    // check if email contains "."
                    if (splitByDot.Length < 2)
                    {
                        Console.WriteLine("Email invalid: no '.' symbol");
                        return false;

                    }else if (splitByDot[0] == "") // check if email has no domain
                    {
                        Console.WriteLine("Email invalid: Email must have domain before '.' symbol");
                        return false;
                    }
                }

                return true;
            }
            
            // generates client id
            public static string GenerateId(string firstName, string lastName) {
                string Id = "";

                // get client initials
                string initials = $"{firstName[0]}{lastName[0]}";   

                // access database
                using(var context = new CarRentalContext())
                {
                    // creates list of clients that have same initials
                    var clientList = context.Client.AsEnumerable()
                        .Where(c  => $"{c.ClientId[0]}{c.ClientId[1]}" == initials)
                        .Select(c => c).ToList();

                    // checks if list is empty
                    if (clientList.Count > 0) {
                        // get's last client on the list
                        Client lastClient = clientList.Last();

                        // creates new id number
                        int idNumber = Int32.Parse(lastClient.ClientId.Remove(0,1)) + 1;
                        Id = $"{initials}{idNumber}";
                    }
                    else
                    {
                        Id = $"{initials}0";
                    }
                }
                return Id;
            }
        }

        // Validates rental entry
        public class RentalValidator
        {
            // generates rental id
            public static int GenerateId()
            {
                int id = 0;

                using (var context = new CarRentalContext())
                {
                    var rentalList = context.Rental
                        .Select(r => r).ToList();

                    // checks the number of rentals in the database
                    if (rentalList.Count > 0)
                    {
                        Rental lastRental = rentalList.Last();

                        id = lastRental.RentalId++; 

                    }      

                }
                return id;
            }

            // get's the current date
            public static string GetDate()
            {
                return DateTime.Now.ToString("dd-MM-yyyy");
            }

            // checks the entered date
            public static bool ValidateDate(string date)
            {
                bool result = false;
                Dictionary<string, int> dateEntered = new Dictionary<string, int>();
                Dictionary<string, int> currentDate = SplitDate(DateTime.Now.ToString("dd-MM-yyyy"));

                try
                {
                    dateEntered = SplitDate(date);

                    // checks if the year entered is greater than or equal to the current year
                    if (dateEntered["year"] >= currentDate["year"])
                    {
                        // checks if the month entered is greater than the current month or
                        // if the month entered is less than the current month but the year entered is greater than the current year
                        if (
                            (dateEntered["month"] >= currentDate["month"]) ||
                            ((dateEntered["month"] <= currentDate["month"]) && 
                            (dateEntered["year"] > currentDate["year"]))
                            )
                        {
                            // checks if the day entered is greater than the current day or
                            // if the day entered is less than the current day but the month entered is greater than the current month
                            if (
                                (dateEntered["day"] > currentDate["day"]) ||
                                ((dateEntered["day"] <= currentDate["day"]) && 
                                (dateEntered["month"] > currentDate["month"]))
                            )
                            {
                                result = true;

                            }else if (
                                    ((dateEntered["day"] <= currentDate["day"]) && // checks if the day entered is less than or equal to the current day
                                    (dateEntered["month"] < currentDate["month"]) && // checks if the month entered is less than the current month
                                    (dateEntered["year"] > currentDate["year"])) // checks if the year entered is greater than the current year
                                )
                            {
                                result = true;
                            }
                        }
                    }


                    // checks if month is below 1 or greater than 12
                    if (dateEntered["month"] < 1 || dateEntered["month"] > 12)
                    {
                        result = false;
                    }

                    // checks if day is below 1 or greater than 31
                    if (dateEntered["day"] == 0 || dateEntered["day"] > 31)
                    {
                        result = false;
                    }

                    // checks if year is not a leap year and day is equialent to 29
                    if( !(IsLeapYear(dateEntered["year"])) && dateEntered["month"] == 2 && dateEntered["day"] == 29 ) 
                    {
                        result = false;
                    }

                    int[] monthsWith30Days = [4, 6, 9, 11];
                    if( dateEntered["day"] > 30 && monthsWith30Days.Contains(dateEntered["month"]) )
                    {
                        result = false;
                    }

                }
                catch
                {
                    Console.WriteLine("Date must follow the format 'dd-MM-yyyy' and cannot contain other symbols or letters");
                }
                

                return result;
            }

            // splits date into day month and year and converts to integer
            private static Dictionary<string, int> SplitDate(string date)
            {
                string[] splitDate = date.Split('-');

                Dictionary<string, int> dateDictionary = new Dictionary<string, int>
                {
                    {"day", Int32.Parse(splitDate[0])},
                    {"month", Int32.Parse(splitDate[1])},
                    {"year", Int32.Parse(splitDate[2])}
                };

                return dateDictionary;
            }

            // checks if a year is a leap year
            private static bool IsLeapYear(int year)
            {
                bool isLeapYear = false;

                // checks if the year is divicible by 4
                if(year % 4 == 0)
                {
                    // checks if the year is divicible by 100
                    if (year % 100 == 0)
                    {
                        // checks if the year is divicible by 400
                        if (year % 400 == 0)
                        {
                            isLeapYear = true;
                        }
                    }
                    else
                    {
                        isLeapYear = true;
                    }
                }
                
                return isLeapYear;
            }
        }
    }
}
