using Holiday.Models;
using Holiday.Tests.Models;

namespace HolidaySearch.Tests.Mappers
{
    public static class FlightMapper
    {
        public static Flight MapToDomain(FlightDto dto)
        {
            return new Flight
            {
                Id = dto.Id,
                Airline = dto.Airline,
                From = dto.From,
                To = dto.To,
                Price = dto.Price,
                DepartureDate = dto.DepartureDate
            };
        }
    }
}
