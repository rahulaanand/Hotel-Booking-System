using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_System_2.Models
{
    public class Hotels
    {
        [Key]
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelLocation { get; set; }
        public decimal Price { get; set; }
        public int RoomAvailability { get; set; }
        public string Amenities { get; set; }

        public ICollection<Rooms>? Rooms { get; set; }
        public ICollection<Staffs>? Staff { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }


    }
}
