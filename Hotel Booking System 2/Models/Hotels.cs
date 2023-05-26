using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_System_2.Models
{
    public class Hotels
    {
        [Key]
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelLocation { get; set; }
        public string HotelDescription { get; set; }
        public decimal Price { get; set; }
        public string Amenities { get; set; }

        public List<Rooms> Rooms { get; set; }
    }
}
