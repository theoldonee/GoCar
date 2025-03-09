namespace GoCar.xUnitTest
{
    public class CarRentalTest
    {
        // Declare field to store an instance of CarRepository 
        private readonly CarRepository _carrEPO;

        public CarRentalTest()
        {
            // create temp db for testing
            var options = new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase(databaseName: "test_database").Options;
            var dbContext = new DatabaseContext(options);
            _carRepo = new CarRepository(dbContext);
        }

        [Fact]
        public void AddCaar_IncreasesAmountofCars()
        {
            // Create new car object
            var car = new Car { CarId="Z345-ZM89", Model = "Zookmobile", Type = "Sport's Car", Year = 2021, Available = true };

            // AddCar method to save car in db
            _carRepo.AddCar(car);

            //store cars in temp db
            var carsInDB = _carRepo.GetCars();
            Assert.Contains(car, carsInDB);
        }
            
    }
}
