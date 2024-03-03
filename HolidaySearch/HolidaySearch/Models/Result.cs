namespace Holiday.Models
{
    public class Result
    {
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
        public decimal TotalPrice => Flight.Price + Hotel.PricePerNight * Hotel.Nights;
    }
}
