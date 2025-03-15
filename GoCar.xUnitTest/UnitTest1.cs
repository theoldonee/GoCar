using Xunit;
using GoCar;

public class CarTests
{
    [Fact]
    public void Car_ShouldBeAdddedWithCorrectInputValues()
    {
        // Arrange ( test data )
        var car = new Car
        {
            CarId = "001",
            Make = "Toyota",
            Model = "Corolla",
            FuelType = "Petrol",
            Type = "Sedan",
            Year = 2006,
            Available = true
        };

        // Act & Assert ( checks if values were set correctly)
        Assert.Equal("001", car.CarId);
        Assert.Equal("Toyota", car.Make);
        Assert.Equal("Corolla", car.Model);
        Assert.Equal("Petrol", car.FuelType);
        Assert.Equal("Sedan", car.Type);
        Assert.Equal(2006, car.Year); 
        Assert.True(car.Available); // Check if available is tru
    }
}
