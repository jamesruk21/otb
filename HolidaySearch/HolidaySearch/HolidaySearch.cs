using Holiday.Models;

namespace Holiday
{
    public class HolidaySearch
    {
        private readonly IEnumerable<Flight> _flights;
        private readonly IEnumerable<Hotel> _hotels;

        public HolidaySearch(IEnumerable<Flight> flights, IEnumerable<Hotel> hotels)
        {
            _flights = flights ?? throw new ArgumentNullException(nameof(flights));
            _hotels = hotels ?? throw new ArgumentNullException(nameof(hotels));
        }

        public IEnumerable<Result> Results(IEnumerable<string> departingFrom, string travelingTo, DateTime departureDate, int duration)
        {
            // Placeholder implementation
            return _flights
                .Where(f => (!departingFrom.Any() || (departingFrom.Any() && departingFrom.Contains(f.From))))
                .Where(f => f.To == travelingTo && f.DepartureDate == departureDate)
                .SelectMany(f =>
                    _hotels.Where(h => h.LocalAirports.Contains(travelingTo))
                    .Where(h => h.ArrivalDate == departureDate && h.Nights == duration)
                    .Select(h => new Result { Flight = f, Hotel = h }))
                .OrderBy(r => r.TotalPrice);
        }
    }
}
