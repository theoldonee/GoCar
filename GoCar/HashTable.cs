﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
//using Internal;

namespace GoCar
{
    // Custom Node class for our linked list
    public class Node<TKey, TValue>
    {
        // Property to store the key associated with this node
        public TKey Key { get; set; }
        // Property to store the value associated with the key
        public TValue Value { get; set; }
        // Reference to the next node in the linked list
        public Node<TKey, TValue> Next { get; set; }

        // Constructor to initialize a new node with a given key and value
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Next = null; // The Next pointer is set to null by default (end of the list)
        }
    } // end of class: Node

    // CLASS: CustomLinkedList; stores key-value pairs using nodes
    public class CustomLinkedList<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>// uses the Node<TKey, TValue> class to link multiple elements in a sequence
    {
        // If the list is empty, _head will be null
        private Node<TKey, TValue> _head;

        // Keeps track of how many nodes are in the linked list
        private int _count;

        // Public read-only property to get the number of elements in the list
        public int Count => _count;

        // Constructor initializes an empty linked list.
        public CustomLinkedList()
        {
            _head = null;
            _count = 0;
        }

        // Add a new node to the end of the list
        public void AddLast(TKey key, TValue value)
        {
            // Create a new node with the given key and value
            Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value);

            // If the list is empty (head is null), the new node becomes the head
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                // If the list is not empty, traverse to the last node
                Node<TKey, TValue> current = _head;

                // Traverse through the list until we reach the last node (where Next is null)
                while (current.Next != null)
                {
                    current = current.Next;
                }
                // Set the Next property of the last node to point to the new node
                current.Next = newNode;
            }
            // Increment the count of nodes in the list
            _count++;

        } // end of method: AddLast

        // Find and remove a node with the matching key in the linked list
        public bool Remove(TKey key)
        {
            // If the list is empty (head is null), nothing to remove
            if (_head == null)
                return false;

            // If the head node is the one to remove (key matches)
            if (_head.Key.Equals(key))
            {
                // Move the head to the next node, effectively removing the head node
                _head = _head.Next;

                // Decrease the count of nodes in the list
                _count--;

                // Return true indicating the node was found and removed
                return true;
            }

            // Search for the node in the rest of the list (starting from the second node)
            Node<TKey, TValue> current = _head;

            // Traverse through the list to find the node with the matching key
            while (current.Next != null)
            {
                // If the next node's key matches the provided key, remove it
                if (current.Next.Key.Equals(key))
                {
                    // Skip the next node by linking the current node to the next node's next
                    current.Next = current.Next.Next;

                    // Decrease the count of nodes in the list
                    _count--;

                    // Return true indicating the node was found and removed
                    return true;
                }

                // Move to the next node
                current = current.Next;
            }

            // If no node with the matching key was found, return false
            return false;

        } // end of method: Remove

        // Find a node with the matching key in the linked list
        public Node<TKey, TValue> Find(TKey key)
        {
            // Start from the head node of the list
            Node<TKey, TValue> current = _head;

            // Traverse through the linked list
            while (current != null)
            {
                // If the current node's key matches the provided key, return this node
                if (current.Key.Equals(key))
                {
                    return current;
                }

                // Move to the next node in the list
                current = current.Next;
            }

            // If no node with the matching key is found, return null
            return null;
        } // end of method: Find


        // search through all nodes in the linked list and perform an action on each node
        // allows custom 'actions'; similar to the functionality of a foreach loop
        public void ForEach(Action<TKey, TValue> action)
        {
            // Start from the head of the list
            Node<TKey, TValue> current = _head;

            // Traverse through the list until the end (when current is null)
            while (current != null)
            {
                // Execute the provided action on the current node's key and value
                action(current.Key, current.Value);

                // Move to the next node in the list
                current = current.Next;
            }
        } // end of method: ForEach

        // Clear the linked list by removing all nodes
        public void Clear()
        {
            // Set the head of the list to null, effectively removing all nodes
            _head = null;

            // Reset the count of nodes to 0, since the list is now empty
            _count = 0;
        } // end of method: Clear

        //
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Node<TKey, TValue> current = _head;

            while (current != null)
            {
                yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                current = current.Next;
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    } // end of class: CustomLinkedList


    // CLASS: HashTable 
    public class HashTable<TKey, TValue>
    {
        // Internal storage using array of linked lists (separate chaining)
        protected CustomLinkedList<TKey, TValue>[] _buckets;

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
            _buckets = new CustomLinkedList<TKey, TValue>[PrimeSizes[_currentPrimeIndex]];
            _count = 0;
        }

        // Custom hash function to distribute keys uniformly
        private int GetHashCode(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null");

            // built-in hash code and ensure non-negative index
            int hashCode = Math.Abs(key.GetHashCode());
            return hashCode % _buckets.Length;
        } // end of method: GetHashCode


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
                    _buckets[bucketIndex] = new CustomLinkedList<TKey, TValue>();
                }

                // find the node with the specified key in the bucket at the calculated index
                var node = _buckets[bucketIndex].Find(key);

                // Check if the node with the specified key already exists
                if (node != null)
                {
                    // If the node exists, update its value
                    node.Value = value;
                    return;
                }

                // If the node does not exist, add a new node with the provided key and value to the bucket
                _buckets[bucketIndex].AddLast(key, value);
                _count++;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during insertion: {ex.Message}");
            }
        } // end of method: Insert

        // Dynamic resizing method with prime number bucket sizes
        private void Resize()
        {
            try
            {
                // Move to the next prime size for the bucket array (ensure we're using a larger size)
                _currentPrimeIndex = Math.Min(_currentPrimeIndex + 1, PrimeSizes.Length - 1);
                // Get the next prime size for the buckets
                int newSize = PrimeSizes[_currentPrimeIndex];

                // Store the current buckets in a temporary variable before resizing
                var oldBuckets = _buckets;

                // Create a new array of buckets with the new size
                _buckets = new CustomLinkedList<TKey, TValue>[newSize];
                _count = 0; // Reset the count of elements as we will rehash and insert them again

                // Rehash all elements from the old buckets and insert them into the new bucket array
                foreach (var bucket in oldBuckets)
                {
                    // Only rehash non-null buckets
                    if (bucket != null)
                    {
                        // For each element in the old bucket, insert it into the new bucket array
                        bucket.ForEach((k, v) => Insert(k, v));
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        } // end of method: Resize

        // Search method with error handling
        public TValue Search(TKey key)
        {
            try
            {
                // Calculate the index of the bucket for the given key using its hash code
                int bucketIndex = GetHashCode(key);
                // Retrieve the bucket at the calculated index
                var bucket = _buckets[bucketIndex];

                if (bucket != null)
                {
                    // Try to find the node with the matching key in the bucket
                    var node = bucket.Find(key);
                    if (node != null)
                    {
                        return node.Value;
                    }
                }
                // Key not found
                throw new KeyNotFoundException($"The key {key} was not found in the hash table.");
            }
            catch (Exception ex)
            {
                return default;
            }
        } // end of method: Search

        // Delete method with error handling
        public bool Delete(TKey key)
        {
            try
            {
                // Calculate the index of the bucket for the given key using its hash code
                int bucketIndex = GetHashCode(key);
                // Retrieve the bucket at the calculated index
                var bucket = _buckets[bucketIndex];

                // Check if the bucket is not null and if the key exists in the bucket (remove the node if found)
                if (bucket != null && bucket.Remove(key))
                {
                    _count--; // Decrement the count of elements since the node was removed
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deletion error: {ex.Message}");
                return false;
            }
        } // end of method: Delete



    } // END of HashTable class

    // hashTable for cars
    public class CarHashTable<TKey> : HashTable<TKey, Car>
    {
        // searche's by selected search option
        public IEnumerable<Car> SearchBy(string value, string searchBy)
        {
         

            // Iterates over bucket
            foreach (var bucket in _buckets)
            {
                // checks if bucket is empty
                if (bucket != null)
                {

                    // iterates over items in bucket
                    foreach (var item in bucket)
                    {
                        Car car = item.Value;

                        // checks if the search is by car fuelType 
                        if (searchBy == "3")
                        {
                            if (car.FuelType == value)
                            {
                                yield return car;
                            }
                        }

                        // checks if the search is by car make
                        if (searchBy == "2")
                        {
                            if (car.Make == value)
                            {
                                yield return car;
                            }
                        }

                        // checks if the search is by car type
                        if (searchBy == "4")
                        {
                            if (car.Type == value)
                            {
                                yield return car;
                            }
                        }

                        // checks if the search is by year
                        if (searchBy == "5")
                        {
                            if (car.Year.ToString() == value)
                            {
                                yield return car;
                            }
                        }

                        // checks if the search is by availability
                        if (searchBy == "6")
                        {
                            if (car.Available == true)
                            {
                                yield return car;
                            }
                        }

                    }
                }
            }


        }

    }

    // hashTable for clients
    public class ClientHashTable<TKey> : HashTable<TKey, Client>
    {
        // searche's by selected search option
        public IEnumerable<Client> SearchBy(string value, string searchBy)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;


            // Iterates over bucket
            foreach (var bucket in _buckets)
            {
                // checks if bucket is empty
                if (bucket != null)
                {
                    // iterates over items in bucket
                    foreach (var item in bucket)
                    {
                        Client client = item.Value;

                        // checks if the search is by initials
                        if (searchBy == "2")
                        {
                            // creates initials
                            string initials = $"{client.FirstName[0]}{client.LastName[0]}";
                            value = value.ToUpper();

                            // checks if initals match the value
                            if (initials == value)
                            {
                                yield return client;
                            }
                        }

                        // checks if the search is by firstname
                        if (searchBy == "3")
                        {
                            value = textInfo.ToTitleCase(value.ToLower());

                            // checks if firstname match the value
                            if (client.FirstName == value)
                            {
                                yield return client;
                            }
                        }

                        // checks if the search is by lastname
                        if (searchBy == "4")
                        {
                            value = textInfo.ToTitleCase(value.ToLower());

                            // checks if lastname match the value
                            if (client.LastName == value)
                            {
                                yield return client;
                            }
                        }
                    }
                }
            }

        }
    }

    // hashTable for rental information
    public class RentaltHashTable<TKey> : HashTable<TKey, Rental>
    {
        // searche's by selected search option
        public IEnumerable<Rental> SearchBy(string value, string searchBy)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            // Iterates over bucket
            foreach (var bucket in _buckets)
            {
                // checks if bucket is empty
                if (bucket != null)
                {
                    // iterates over items in bucket
                    foreach (var item in bucket)
                    {
                        Rental rental = item.Value;

                        // checks if the search is by collectionDate
                        if (searchBy == "2")
                        {

                            // checks if collectionDate match the value
                            if (rental.CollectionDate == value)
                            {
                                yield return rental;
                            }
                        }

                        // checks if the search is by returnDate
                        if (searchBy == "3")
                        {

                            // checks if returnDate match the value
                            if (rental.ReturnDate == value)
                            {
                                yield return rental;
                            }
                        }

                        // checks if the search is by carId
                        if (searchBy == "4")
                        {

                            // checks if CarId match the value
                            if (rental.CarId == value)
                            {
                                yield return rental;
                            }
                        }

                        // checks if the search is by clientId
                        if (searchBy == "5")
                        {
                            // checks if ClientId match the value
                            if (rental.ClientId.Equals(value))
                            {
                                yield return rental;
                            }
                        }

                        if (searchBy == "6")
                        {
                            // checks if ClientId match the value
                            if ($"{rental.RentalId[0]}" == value)
                            {
                                yield return rental;
                            }
                        }

                    }
                }
            }

        }
    }

}
