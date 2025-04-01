using System;
using System.Collections.Generic;
//using Internal;

namespace GoCar
{

    public class HashTable<TKey, TValue>
    {
        // Internal storage using array of linked lists (separate chaining)
        protected LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;

        // Prime numbers for bucket sizing to reduce collisions
        private static readonly int[] PrimeSizes = new int[]
        {
            11, 23, 47, 97, 197, 401, 809, 1627, 3251, 6521, 13049, 26099, 52213
        };

        // Tracking table metrics
        private int _count;
        private int _currentPrimeIndex;
        private const double LoadFactorThreshold = 0.75;

        // Additional utility methods
        public int Count => _count;
        public int BucketCount => _buckets.Length;
        public double LoadFactor => (double)_count / _buckets.Length;

        public HashTable()
        {
            _currentPrimeIndex = 0;
            _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[PrimeSizes[_currentPrimeIndex]];
            _count = 0;
        }

        // Custom hash function to distribute keys uniformly
        private int GetHashCode(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null");

            // Use built-in hash code and ensure non-negative index
            int hashCode = Math.Abs(key.GetHashCode());
            return hashCode % _buckets.Length;
        }

        
        // Insert a key-value pair with dynamic resizing and update if key exists
        public void Insert(TKey key, TValue value)
        {
            try
            {
                // Check load factor and resize if necessary
                if ((double)_count / _buckets.Length >= LoadFactorThreshold)
                {
                    Resize();
                }

                int bucketIndex = GetHashCode(key);

                // Initialize bucket if null
                if (_buckets[bucketIndex] == null)
                {
                    _buckets[bucketIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
                }

                // Check for existing key and update value i
                var bucket = _buckets[bucketIndex];
                foreach (var item in bucket)
                {
                    if (item.Key.Equals(key))
                    {
                        // Remove the existing key-value pair and insert the new value
                        bucket.Remove(item);
                        bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
                        return; // Exit early since update is complete
                    }
                }

                // Add new key-value pair if no duplicate was found
                bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
                _count++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during insertion: {ex.Message}");
            }
        }

        // Dynamic resizing method with prime number bucket sizes
        private void Resize()
        {
            try
            {
                // Move to next prime size
                _currentPrimeIndex = Math.Min(_currentPrimeIndex + 1, PrimeSizes.Length - 1);
                int newSize = PrimeSizes[_currentPrimeIndex];

                // Create new buckets and rehash existing elements
                var oldBuckets = _buckets;
                _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[newSize];
                _count = 0;

                foreach (var bucket in oldBuckets)
                {
                    if (bucket != null)
                    {
                        foreach (var kvp in bucket)
                        {
                            Insert(kvp.Key, kvp.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Resize operation failed: {ex.Message}");
                throw; // Re-throw to allow caller to handle
            }
        }

        // Search method with error handling
        public TValue Search(TKey key)
        {
            try
            {
                int bucketIndex = GetHashCode(key);
                var bucket = _buckets[bucketIndex];

                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        if (item.Key.Equals(key))
                        {
                            return item.Value;
                        }
                    }
                }

                // Key not found
                throw new KeyNotFoundException($"The key {key} was not found in the hash table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Search error: {ex.Message}");
                // Depending on requirements, you might return default(TValue) or re-throw
                return default;
            }
        }

        // Delete method with error handling
        public bool Delete(TKey key)
        {
            try
            {
                int bucketIndex = GetHashCode(key);
                var bucket = _buckets[bucketIndex];

                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        if (item.Key.Equals(key))
                        {
                            bucket.Remove(item);
                            _count--;
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deletion error: {ex.Message}");
                return false;
            }
        }

     //CHANGES
        // DISPLAY ALL OBJECTS IN HASHTABLE
        public void DisplayAll()
        {
        }

        
    }

    public class CarHashTable<TKey> : HashTable<TKey, Car>
    {
        public List<Car> SearchByFuelType(string fuelType)
        {
            List<Car> carList = new List<Car>();
            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        Car car = item.Value;
                        if (car.FuelType == fuelType)
                        {
                            carList.Add(car);
                        }
                    }
                }
            }

            return carList;

        }


    }
}
