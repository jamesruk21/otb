using Holiday.Models;
using Holiday.Tests.Models;

namespace HolidaySearch.Tests.Mappers
{
    public static class HotelMapper
    {
        public static Hotel MapToDomain(HotelDto dto)
        {
            return new Hotel
            {
                Id = dto.Id,
                Name = dto.Name,
                ArrivalDate = dto.ArrivalDate,
                PricePerNight = dto.PricePerNight,
                LocalAirports = dto.LocalAirports,
                Nights = dto.Nights
            };
        }
    }
}
