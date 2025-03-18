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
                foreach (char items in stringArray)
                {
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
                if (year > 1999 && year <= currentYear)
                {
                    result = true;
                }
                else
                {
                    if(year > currentYear)
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
            public static bool ValidatePhoneNumber(int phoneNumber)
            {
                string phoneNo = phoneNumber.ToString();
                
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
                using(var context = new CarRentalContex())
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
            public static int GenerateId()
            {
                int Id = 0;

                using (var context = new CarRentalContex())
                {
                    var rentalList = context.Rental
                        .Select(r => r).ToList();

                    if (rentalList.Count > 0)
                    {
                        Rental lastRental = rentalList.Last();

                        Id = lastRental.RentalId++; 

                    }      

                }
                return Id;
            }

            public static string GetDate()
            {
                return DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
    }
}
