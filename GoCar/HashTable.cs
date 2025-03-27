using System;
using System.Collections.Generic;
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

        //REMOVE
        public HashTable()
        {
            table = new Node<TKey, TValue>[Size];
        }

        //REMOVE
        // Hash function to compute index based on key
        private int Hash(TKey key)
        {
            int hashValue = key.GetHashCode() % Size;
            return Math.Abs(hashValue); // Ensure non-negative index
        }
//CHANGES//
        // ADD DATA OBJECT TO HASHTABLE
        public void Add(TKey key, TValue value)
        {
            int index = Hash(key);

            // Check if key already exists and update if found
            Node<TKey, TValue> current = table[index];
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    current.Value = value; // Update existing key-value pair
                    return;
                }
                current = current.Next;
            }

            // Insert new node at the start of the linked list
            Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value);
            newNode.Next = table[index];
            table[index] = newNode;
        }

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
