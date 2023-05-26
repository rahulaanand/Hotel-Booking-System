using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_System_2.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Customers Customer { get; set; }
        public Hotels Hotels{ get; set; }
    }
}
