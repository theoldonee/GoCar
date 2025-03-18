using Xunit;
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

}

