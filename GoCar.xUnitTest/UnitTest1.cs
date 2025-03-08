namespace GoCar.xUnitTest
{
    public class CarRentalTest()
    {

        [Fact]
        public void RegisterUser_ShouldBeValid()
        {
            var user = new User {email = "zook@hotmail.com", password = "zook123"};
            Assert.True(result);
        }
    }
}
