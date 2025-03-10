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
        }

        // Validates client info
        public class ClientValidator
        {

        }

        // Validates rental entry
        public class RentalValidator
        {

        }
    }
}
