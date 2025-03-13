using System;
using System.Collections.Generic;

namespace GoCar
{
    public class HashTable
    {
        private const int Size = 100; // Size of the hash table
        private List<Car>[] table;   // Array of linked lists to handle collisions

        public HashTable()
        {
            table = new List<Car>[Size];
            for (int i = 0; i < Size; i++)
            {
                table[i] = new List<Car>();
            }
        }

        // HASH FUNCTION to convert CarId to an index in the array
        private int Hash(string carId)
        {
            int hashValue = 0;
            foreach (char c in carId)
            {
                hashValue = (hashValue * 31 + c) % Size;
            }
            return hashValue;
        }

        // ADDING CAR OBJECT to the hash table
        public void AddCar(Car car)
        {
            int index = Hash(car.CarId);
            // If the CarId already exists, replace the existing car
            var existingCar = FindCar(car.CarId);
            if (existingCar != null)
            {
                RemoveCar(car.CarId);  // Remove the old car before adding the new one
            }
            table[index].Add(car);
        }

        // SEARCH FOR CAR BY CarId
        public Car FindCar(string carId)
        {
            int index = Hash(carId);
            foreach (var car in table[index])
            {
                if (car.CarId == carId)
                {
                    return car;
                }
            }
            return null;  // Car not found
        }

        // REMOVE FOR CAR BY CarId
        public bool RemoveCar(string carId)
        {
            int index = Hash(carId);
            var car = FindCar(carId);
            if (car != null)
            {
                table[index].Remove(car); // Remove the car from the list
                return true;
            }
            return false;  // Car not found
        }

        // DISPLAY CARS STORED IN HASHTABLE (for debugging purposes)
        public void DisplayAllCars()
        {
            for (int i = 0; i < Size; i++)
            {
                if (table[i].Count > 0)
                {
                    foreach (var car in table[i])
                    {
                        Console.WriteLine($"CarId: {car.CarId}, Make: {car.Make}, Model: {car.Model}");
                    }
                }
            }
        }
    }
}
