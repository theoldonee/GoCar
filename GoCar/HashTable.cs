using System;
using System.Collections.Generic;
using Internal;

namespace GoCar
{

    public class HashTable<TKey, TValue>
    {
        // Internal storage using array of linked lists (separate chaining)
        private LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;

        // Prime numbers for bucket sizing to reduce collisions
        private static readonly int[] PrimeSizes = new int[]
        {
            11, 23, 47, 97, 197, 401, 809, 1627, 3251, 6521, 13049, 26099, 52213
        };

        // Tracking table metrics
        private int _count;
        private int _currentPrimeIndex;
        private const double LoadFactorThreshold = 0.75;

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

    //CHANGES//
        // SEARCH FOR OBJECT BY KEY
        public TValue Find(TKey key)
        {
            int index = Hash(key);
            Node<TKey, TValue> current = table[index];

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return default; // Return default value if key is not found
        }

        // REMOVE OBJECT BY KEY
        public bool Remove(TKey key)
        {
            int index = Hash(key);
            Node<TKey, TValue> current = table[index];
            Node<TKey, TValue> previous = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (previous == null) // If it's the first node
                    {
                        table[index] = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false; // Key not found
        }

        // DISPLAY ALL OBJECTS IN HASHTABLE
        public void DisplayAll()
        {
            for (int i = 0; i < Size; i++)
            {
                Node<TKey, TValue> current = table[i];
                if (current != null)
                {
                    Console.Write($"Bucket {i}: ");
                    while (current != null)
                    {
                        Console.Write($"[{current.Key} -> {current.Value}] -> ");
                        current = current.Next;
                    }
                    Console.WriteLine("NULL");
                }
            }
        }
    }
}
