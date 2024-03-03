namespace Holiday.Tests
{
    public class HolidaySearchTests
    {
        [Fact]
        public void Results_Returns_BestValueHoliday_Customer1()
        {
            // Arrange
            var holidaySearch = new HolidaySearch();
            var departingAirport = new List<string> { "MAN" };

            // Act
            var results = holidaySearch.Results(departingAirport, "AGP", new DateTime(2023, 7, 1), 7);

            // Assert
            Assert.Equal(2, results.First().Flight.Id);
            Assert.Equal(9, results.First().Hotel.Id);
        }

        [Fact]
        public void Results_Returns_BestValueHoliday_Customer2()
        {
            // Arrange
            var holidaySearch = new HolidaySearch();
            var departingAirport = new List<string> { "LTN", "LGW" };

            // Act
            var results = holidaySearch.Results(departingAirport, "PMI", new DateTime(2023, 6, 15), 10);

            // Assert
            Assert.Equal(6, results.First().Flight.Id);
            Assert.Equal(5, results.First().Hotel.Id);
        }

        [Fact]
        public void Results_Returns_BestValueHoliday_Customer3()
        {
            // Arrange
            var holidaySearch = new HolidaySearch();
            var departingAirport = new List<string>();

            // Act
            var results = holidaySearch.Results(departingAirport, "LPA", new DateTime(2022, 11, 10), 14);

            // Assert
            Assert.Equal(7, results.First().Flight.Id);
            Assert.Equal(6, results.First().Hotel.Id);
        }
    }
}
