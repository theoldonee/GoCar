using System;
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
    }

    // CLASS: CustomLinkedList; stores key-value pairs using nodes
    public class CustomLinkedList<TKey, TValue> // uses the Node<TKey, TValue> class to link multiple elements in a sequence
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

        } // end of method AddLast

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

        } // end of method Remove

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
        } // end of method Node



    } // end of CustomLinkedList


    // CLASS: HashTable 
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

        
    } // END of HashTable class

    // hashTable for cars
    public class CarHashTable<TKey> : HashTable<TKey, Car>
    {
        // searche's by selected search option
        public List<Car> SearchBy(string value, string searchBy)
        {
            List<Car> carList = new List<Car>();

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
                        if(searchBy == "fuelType")
                        {
                            if (car.FuelType == value)
                            {
                                carList.Add(car);
                            }
                        }

                        // checks if the search is by car make
                        if (searchBy == "make")
                        {
                            if (car.Make == value)
                            {
                                carList.Add(car);
                            }
                        }

                        // checks if the search is by car type
                        if (searchBy == "type")
                        {
                            if (car.Type == value)
                            {
                                carList.Add(car);
                            }
                        }

                        // checks if the search is by year
                        if (searchBy == "year")
                        {
                            if (car.Year.ToString() == value)
                            {
                                carList.Add(car);
                            }
                        }

                        // checks if the search is by availability
                        if (searchBy == "availabe")
                        {
                            if (car.Available == true)
                            {
                                carList.Add(car);
                            }
                        }

                    }
                }
            }

            return carList;

        }

    }

    // hashTable for clients
    public class ClientHashTable<TKey> : HashTable<TKey, Client>
    {
        // searche's by selected search option
        public List<Client> SearchBy(string value, string searchBy)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            List<Client> clientList = new List<Client>();

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
                        if (searchBy == "initials")
                        {
                            // creates initials
                            string initials = $"{client.FirstName[0]}{client.LastName[0]}";
                            value = value.ToUpper();

                            // checks if initals match the value
                            if(initials == value)
                            {
                                clientList.Add(client);
                            }
                        }

                        // checks if the search is by firstname
                        if (searchBy == "firstname")
                        {
                            value = textInfo.ToTitleCase(value.ToLower());

                            // checks if firstname match the value
                            if (client.FirstName == value)
                            {
                                clientList.Add(client);
                            }
                        }

                        // checks if the search is by lastname
                        if (searchBy == "lastname")
                        {
                            value = textInfo.ToTitleCase(value.ToLower());

                            // checks if lastname match the value
                            if (client.LastName == value)
                            {
                                clientList.Add(client);
                            }
                        }
                    }
                }
            }
            
            return clientList;
        }
    }

    // hashTable for rental information
    public class RentaltHashTable<TKey> : HashTable<TKey, Rental>
    {
        // searche's by selected search option
        public List<Rental> SearchBy(string value, string searchBy)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            List<Rental> rentalList = new List<Rental>();

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
                        if (searchBy == "collectionDate")
                        {

                            // checks if collectionDate match the value
                            if (rental.CollectionDate == value)
                            {
                                rentalList.Add(rental);
                            }
                        }

                        // checks if the search is by returnDate
                        if (searchBy == "returnDate")
                        {

                            // checks if returnDate match the value
                            if (rental.ReturnDate == value)
                            {
                                rentalList.Add(rental);
                            }
                        }

                        // checks if the search is by carId
                        if (searchBy == "carId")
                        {

                            // checks if CarId match the value
                            if (rental.CarId == value)
                            {
                                rentalList.Add(rental);
                            }
                        }

                        // checks if the search is by clientId
                        if (searchBy == "clientId")
                        {

                            // checks if ClientId match the value
                            if (rental.ClientId == value)
                            {
                                rentalList.Add(rental);
                            }
                        }
                    }
                }
            }

            return rentalList;
        }
    }

}
