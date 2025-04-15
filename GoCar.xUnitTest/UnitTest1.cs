using GoCar;

public class CarTests
{
    // Positive test cases for carId
    [Theory]
    [InlineData("Y664-AA69")]
    [InlineData("Z555-BB55")]
    public void ValidatecarId_ShouldReturnTrue(string carId)
    {
        bool result = Validator.CarValidator.ValidateId(carId);
        Assert.True(result);
    }

    // Negative test cases for carId
    [Theory]
    [InlineData("1234-5678")] // Only numbers
    [InlineData("YYYY-9999")] // Invalid format
    [InlineData("XX-1234")]   // Wrong length
    public void ValidateCarId_InvalidCases_ShouldReturnFalse(string carId)
    {
        bool result = Validator.CarValidator.ValidateId(carId);
        Assert.False(result);
    }

    // Positive test cases for fueltypes
    [Theory]
    [InlineData("Diesel")]
    [InlineData("Petrol")]
    [InlineData("Electric")]
    [InlineData("Hybrid")]
    public void ValidateFuelTyps_ShouldReturnT(string fuelType)
    {
        bool result = Validator.CarValidator.ValidateFuelType(fuelType);
        Assert.True(result);
    }

    // Negative test cases for fueltypes
    [Theory]
    [InlineData("Water")]
    [InlineData("Gasoline123")]
    [InlineData("")]
    public void ValidateFuelType_InvalidCases_ShouldReturnFalse(string fuelType)
    {
        bool result = Validator.CarValidator.ValidateFuelType(fuelType);
        Assert.False(result);
    }

    // Positive test case for year
    [Theory]
    [InlineData(2000)] // Realistic past year
    [InlineData(2025)] // Current year
    public void ValidateYear_ValidCases_ShouldReturnTrue(int year)
    {
        bool result = Validator.CarValidator.ValidateYear(year);
        Assert.True(result);
    }

    // Negative test case for year
    [Theory]
    [InlineData(2027)] // Future year
    [InlineData(1488)] // Unrealistic past year
    public void ValidateYear_InvalidCases_ShouldReturnFalse(int year)
    {
        bool result = Validator.CarValidator.ValidateYear(year);
        Assert.False(result);
    }

    //Positive test case for phone number
    [Theory]
    [InlineData(12345678)]  // Valid phone number (8 digits exactly)
    [InlineData(87654321)]  // Another valid phone number (8 digits exactly)
    public void ValidatePhoneNumber_ShouldReturnTrue(int phoneNumber)
    {
        bool result = Validator.ClientValidator.ValidatePhoneNumber(phoneNumber);
        Assert.True(result);
    }

    // Negative test case for phone number
    [Theory]
    [InlineData(12345)]  // Too short for a phone number (<8 digits)
    [InlineData(0123456789)] // Too long for a phone number (>8 digits)
    public void ValidatePhoneNumber_ShouldReturnFalse(int phoneNumber)
    {
        bool result = Validator.ClientValidator.ValidatePhoneNumber(phoneNumber);
        Assert.False(result);
    }

    // Positive test case for client email
    [Theory]
    [InlineData("zook@gmail.com")] // Correct email format
    [InlineData("zook.cat@gmail.mu")]  // Correct email format
    [InlineData("hello@world.co.uk")]  //COrrect email format
    public void ValidateEmail_ShouldReturnTrue_ForValidEmails (string email)
    {
        bool result = Validator.ClientValidator.ValidateEmail(email);
        Assert.True(result);
    }


    // Negative test case forclient email
    [Theory]
    [InlineData("flookfailcat.com")]  // No @
    [InlineData("flook@com")]  // No "."
    [InlineData("flook@@gmail.com")]  // Multiple "@"s
    [InlineData("@gmail.com")]  // No text before "@"
    [InlineData("flook@.com")]  // No email domain before "."
    [InlineData("flook@gmailcom")]  // No "." in email domain
    public void ValidateEmail_ShouldReturnFalse_ForInvalidEmails(string email)
    {
        bool result = Validator.ClientValidator.ValidateEmail(email);
        Assert.False(result);
    }

    //  Positive test for future dates 
    [Theory]
    [InlineData("25-12-2030")]  // Future holiday
    [InlineData("01-01-2100")]  // Distant future
    [InlineData("29-02-2028")]  // Future leap year
    [InlineData("31-12-2025")]  // End of current year
    public void ValidateDate_ShouldReturnTrueForValidFutureDates(string date)
    {
        bool result = Validator.RentalValidator.ValidateDate(date);
        Assert.True(result);
    }

    // Negative test for past dates 
    [Theory]
    [InlineData("01-01-2000")]  // Past century
    [InlineData("15-03-2015")]  // Random past date
    [InlineData("31-12-1999")]  // End of past century
    [InlineData("28-02-2025")]  // Last day of February 2025
    public void ValidateDate_ShouldReturnFalseForPastDates(string date)
    {
        bool result = Validator.RentalValidator.ValidateDate(date);
        Assert.False(result);
    }

    // Invalid day 
    [Theory]
    [InlineData("32-12-2025")]  // Day > 31
    [InlineData("00-12-2025")]  // Day = 0
    [InlineData("31-04-2026")]  // April has only 30 days
    [InlineData("29-02-2025")]  // Non-leap year, so it should have only 28 days
    public void ValidateDate_ShouldReturnFalseForInvalidDays(string date)
    {
        bool result = Validator.RentalValidator.ValidateDate(date);
        Assert.False(result);
    }

    // Invalid month
    [Theory]
    [InlineData("12-13-2025")] // Month > 12
    [InlineData("12-00-2025")] // Month = 0
    public void ValidateDate_ShouldReturnFalseForInvalidMonths(string date)
    {
        bool result = Validator.RentalValidator.ValidateDate(date);
        Assert.False(result);
    }

    // Incorrect date format 
    [Theory]
    [InlineData("2025-01-01")] // yyyy-MM-dd instead of dd-MM-yyyy
    [InlineData("01/01/2025")] // Uses "/" instead of "-"
    [InlineData("1-1-2025")] // Missing zeros
    [InlineData("January 1, 2025")] // Pure text format
    public void ValidateDate_ShouldReturnFalseForIncorrectFormat(string date)
    {
        bool result = Validator.RentalValidator.ValidateDate(date);
        Assert.False(result);
    }

    // Completely invalid input 
    [Theory]
    [InlineData("abcd-ef-ghij")] // Nonsense string
    [InlineData("12345678")] // Numbers without separators
    [InlineData("")] // Empty string
    public void ValidateDate_ShouldReturnFalseForInvalidInputs(string date)
    {
        bool result = Validator.RentalValidator.ValidateDate(date);
        Assert.False(result);
    }

    // Test for no clients in the hash table
    [Fact]
    public void GenerateClientId_ShouldReturnInitials0_WhenNoClientsExist()
    {
        // Mock the ClientHashTable and simulate no clients with the given initials
        var mockHashTable = new ClientHashTable<string>();

        // Generate ID and expect it to be "AB0" since no clients exist
        var result = Validator.ClientValidator.GenerateId("A", "B", mockHashTable);
        Assert.Equal("AB0", result);
    }

    // Test for empty rental hash table
    [Fact]
    public void GenerateRentalId_ShouldReturnR0_WhenNoRentalsExist()
    {
        // Mock the RentaltHashTable and simulate no rentals with prefix "R"
        var mockHashTable = new RentaltHashTable<string>();

        // Generate ID and expect it to be "R0" since no rentals exist
        var result = Validator.RentalValidator.GenerateId(mockHashTable);
        Assert.Equal("R0", result);
    }

    // Test when the client list contains only one client
    [Fact]
    public void GenerateClientId_ShouldReturnIncrementedId_WhenOneClientExists()
    {
        // Mock the hash table to return one client with ID "YZ100"
        var mockHashTable = new ClientHashTable<string>();

        mockHashTable.Insert("YZ100", new Client { ClientId = "YZ100", FirstName = "Y", LastName = "Z" });
        // Expect the next ID to increment to "YZ101"
        var result = Validator.ClientValidator.GenerateId("Y", "Z", mockHashTable);
        Assert.Equal("YZ101", result);
    }

    // Test when the rental list has only one rental
    [Fact]
    public void GenerateRentalId_ShouldReturnIncrementedId_WhenOneRentalExists()
    {
        // Mock the hash table to return one rental with ID "R1"
        var mockHashTable = new RentaltHashTable<string>();

        mockHashTable.Insert("R1", new Rental { RentalId = "R1" });

        // Expect the next ID to increment to "R2"
        var result = Validator.RentalValidator.GenerateId(mockHashTable);
        Assert.Equal("R2", result);
    }

    // Test when the last client ID is over 100
    [Fact]
    public void GenerateClientId_ShouldHandleMultipleDigits_WhenClientIdOver100()
    {
        // Mock clients with IDs ending in 100, 101, and 102
        var mockHashTable = new ClientHashTable<string>();
        mockHashTable.Insert("YZ100", new Client { ClientId = "YZ100", FirstName = "Y", LastName = "Z"});
        mockHashTable.Insert("YZ101", new Client { ClientId = "YZ101", FirstName = "Y", LastName = "Z" });
        mockHashTable.Insert("YZ102", new Client { ClientId = "YZ102", FirstName = "Y", LastName = "Z" });

        // Expect the next ID to be "YZ103"
        var result = Validator.ClientValidator.GenerateId("Y", "Z", mockHashTable);
        Assert.Equal("YZ103", result);
    }

    // Test when the last rental ID is over 100
    [Fact]
    public void GenerateRentalId_ShouldHandleMultipleDigits_WhenRentalIdOver100()
    {
        // Mock rentals with IDs ending in 100 and 101
        var mockHashTable = new RentaltHashTable<string>();
    
        mockHashTable.Insert("R100", new Rental { RentalId = "R100" });
        mockHashTable.Insert("R100", new Rental { RentalId = "R101" });

        // Expect the next ID to be "R102"
        var result = Validator.RentalValidator.GenerateId(mockHashTable);
        Assert.Equal("R102", result);
    }

    // Test when there are a large number of rental IDs
    [Fact]
    public void GenerateRentalId_ShouldHandleLargeNumberOfRentals()
    {
        var hashTable = new RentaltHashTable<string>();
        for (int i = 0; i <= 999; i++)
        {
            hashTable.Insert($"R{i}", new Rental { RentalId = $"R{i}" });
        }

        var result = Validator.RentalValidator.GenerateId(hashTable);

        Assert.Equal("R1000", result);
    }

}