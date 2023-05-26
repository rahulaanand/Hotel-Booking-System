using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_System_2.Models
{
    public class Rooms
    {
        [Key]
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public bool RoomAvailability { get; set; }
        public Hotels Hotel { get; set; }
        public List<Staffs> Staffs { get; set; }
    }
}
