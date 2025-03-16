using Xunit;
using GoCar;

public class CarTests
{
    [Fact]
    public void Car_ShouldBeAddedWithCorrectInputValues()
    {
        // Arrange (test data)
        var carId = "Y798-AA12";  // Valid format
        var make = "Toyota";
        var model = "Corolla";
        var fuelType = "Petrol";
        var type = "Sedan";
        var year = 2006;
        var available = true;

        // Act (validate )
        bool isValidId = Validator.CarValidator.ValidateId(carId);

        // Assert (check if validations pass)
        Assert.True(isValidId, "CarId invalid");
        // Create car object
        var car = new Car
        {
            CarId = carId,
            Make = make,
            Model = model,
            FuelType = fuelType,
            Type = type,
            Year = year,
            Available = available
        };

        // Assert (make sure values were set correctly)
        Assert.Equal(carId, car.CarId);
        Assert.Equal(make, car.Make);
        Assert.Equal(model, car.Model);
        Assert.Equal(fuelType, car.FuelType);
        Assert.Equal(type, car.Type);
        Assert.Equal(year, car.Year);
        Assert.True(car.Available);
    }

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
