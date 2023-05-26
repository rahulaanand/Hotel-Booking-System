using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_System_2.Models
{
    public class Staffs
    {
        [Key]
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int? HotelId { get; set; }
        public Hotels Hotel { get; set; }
    }
}
