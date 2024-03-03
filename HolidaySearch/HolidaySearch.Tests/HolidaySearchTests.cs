using Holiday.Models;
using Holiday.Tests.Models;
using HolidaySearch.Tests.Mappers;
using Newtonsoft.Json;

namespace Holiday.Tests
{
    public class HolidaySearchTests
    {
        private readonly IEnumerable<Flight> _flights;
        private readonly IEnumerable<Hotel> _hotels;

        public HolidaySearchTests()
        {
            _flights = GetFlights();
            _hotels = GetHotels();
        }

        private IEnumerable<Flight> GetFlights()
        {
            string json = LoadJson("flights.json");
            List<FlightDto> flightDtos = JsonConvert.DeserializeObject<List<FlightDto>>(json);
            return flightDtos.Select(FlightMapper.MapToDomain).AsEnumerable();
        }

        private IEnumerable<Hotel> GetHotels()
        {
            string json = LoadJson("hotels.json");
            IEnumerable<HotelDto> hotelDtos = JsonConvert.DeserializeObject<IEnumerable<HotelDto>>(json);
            return hotelDtos.Select(HotelMapper.MapToDomain).AsEnumerable();
        }

        private string LoadJson(string fileName)
        {
            // Load JSON file content
            string filePath = Path.Combine("TestData", fileName);
            return File.ReadAllText(filePath);
        }

        [Fact]
        public void Results_Returns_BestValueHoliday_Customer1()
        {
            // Arrange
            var holidaySearch = new HolidaySearch(_flights, _hotels);
            var departingFrom = new List<string> { "MAN" };

            // Act
            var results = holidaySearch.Results(departingFrom, "AGP", new DateTime(2023, 7, 1), 7);

            // Assert
            Assert.Equal(826, results.First().TotalPrice);

            var expectedFlightObject = _flights.First(o => o.Id == 2);
            Assert.Equivalent(expectedFlightObject, results.First().Flight);
 
            var expectedHotelObject = _hotels.First(o => o.Id == 9);
            Assert.Equivalent(expectedHotelObject, results.First().Hotel);

            Assert.Single(results);

        }
        [Fact]
        public void Results_Returns_BestValueHoliday_Customer2()
        {
            // Arrange
            var holidaySearch = new HolidaySearch(_flights, _hotels);
            var departingFrom = new List<string> { "LTN", "LGW" };

            // Act
            var results = holidaySearch.Results(departingFrom, "PMI", new DateTime(2023, 6, 15), 10);

            // Assert
            Assert.Equal(675, results.First().TotalPrice);

            var expectedFlightObject = _flights.First(o => o.Id == 6);
            Assert.Equivalent(expectedFlightObject, results.First().Flight);

            var expectedHotelObject = _hotels.First(o => o.Id == 5);
            Assert.Equivalent(expectedHotelObject, results.First().Hotel);

            Assert.Equal(4, results.Count());

            //Check last is correct to test ordering
            Assert.Equal(3103, results.Last().TotalPrice);

            var expectedLastFlightObject = _flights.Last(o => o.Id == 4);
            Assert.Equivalent(expectedLastFlightObject, results.Last().Flight);

            var expectedLastHotelObject = _hotels.Last(o => o.Id == 13);
            Assert.Equivalent(expectedLastHotelObject, results.Last().Hotel);
        }

        [Fact]
        public void Results_Returns_BestValueHoliday_Customer3()
        {
            // Arrange
            var holidaySearch = new HolidaySearch(_flights, _hotels);
            var departingFrom = new List<string>();

            // Act
            var results = holidaySearch.Results(departingFrom, "LPA", new DateTime(2022, 11, 10), 14);

            // Assert
            Assert.Equal(1175, results.First().TotalPrice);

            var expectedFlightObject = _flights.First(o => o.Id == 7);
            Assert.Equivalent(expectedFlightObject, results.First().Flight);

            var expectedHotelObject = _hotels.First(o => o.Id == 6);
            Assert.Equivalent(expectedHotelObject, results.First().Hotel);

            Assert.Single(results);
        }

        [Fact]
        public void Constructor_Throws_Exception_Flights_IsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HolidaySearch(null, _hotels));
        }

        [Fact]
        public void Constructor_Throws_Exception_Hotels_IsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HolidaySearch(_flights, null));
        }

        [Theory]
        [InlineData(new string[] { "ABC" }, "AGP", "2023-07-01", 7)]
        [InlineData(new string[] { "MAN" }, "ABC", "2023-07-01", 7)]
        [InlineData(new string[] { "MAN" }, "AGP", "2023-06-01", 7)]
        [InlineData(new string[] { "MAN" }, "AGP", "2023-07-01", 21)]
        [InlineData(new string[] { }, "ABC", "2023-07-01", 7)]
        [InlineData(new string[] { }, null, "1980-01-01", 7)]
        [InlineData(new string[] { }, null, "2023-07-01", 0)]
        public void Results_Returns_Empty_For_Holiday_NotFound(IEnumerable<string> departingFrom, string travelingTo, DateTime departureDate, int duration)
        {
            // Arrange
            var holidaySearch = new HolidaySearch(_flights, _hotels);

            // Act
            var results = holidaySearch.Results(departingFrom, travelingTo, departureDate, duration);

            // Assert
            Assert.Empty(results);
        }
    }
}
