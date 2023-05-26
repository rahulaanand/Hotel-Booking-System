using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_System_2.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public Hotels? Hotels { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
