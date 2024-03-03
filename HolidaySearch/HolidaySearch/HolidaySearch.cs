using Holiday.Models;

namespace Holiday
{
    public class HolidaySearch
    {
        private readonly IEnumerable<Flight> _flights;
        private readonly IEnumerable<Hotel> _hotels;

        public HolidaySearch(IEnumerable<Flight> flights, IEnumerable<Hotel> hotels)
        {
            _flights = flights;
            _hotels = hotels;
        }

        public IEnumerable<Result> Results(IEnumerable<string> departingFrom, string travelingTo, DateTime departureDate, int duration)
        {
            // Placeholder implementation
            return Enumerable.Empty<Result>();
        }
    }
}
