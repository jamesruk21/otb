using Newtonsoft.Json;


namespace Holiday.Tests.Models
{
    public class HotelDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("price_per_night")]
        public decimal PricePerNight { get; set; }

        [JsonProperty("local_airports")]
        public List<string> LocalAirports { get; set; }

        [JsonProperty("nights")]
        public int Nights { get; set; }
    }
}
