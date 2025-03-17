using Xunit;
using GoCar;

public class CarTests
{
    [Theory]
    [InlineData("Y798-AA12", true)]  // Valid carId
    [InlineData("1234-5678", false)] // Invalid CarId format
    [InlineData("YYYY-9999", false)] // Invalid CarId format
    [InlineData("XX-1234", false)]   // Invalid CarId length
    public void ValidatecarId_ShouldReturnCorrectResult(string carId, bool expected)
    {
        bool result = Validator.CarValidator.ValidateId(carId);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Diesel", true)]
    [InlineData("Petrol", true)]
    [InlineData("Electric", true)]
    [InlineData("Hybrid", true)]
    [InlineData("Water", false)]  // Invalid fueltype
    public void ValidateFuelType_ShouldReturnCorrectResult(string fuelType, bool expected)
    {
        bool result = Validator.CarValidator.ValidateFuelType(fuelType);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2010, true)]  // Valid year
    [InlineData(2145, false)] // Invalid year 
    public void ValidateYear_ShouldReturnCorrectResult(int year, bool expected)
    {
        bool result = Validator.CarValidator.ValidateYear(year);
        Assert.Equal(expected, result);
    }
}
