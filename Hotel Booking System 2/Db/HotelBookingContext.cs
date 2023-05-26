using Hotel_Booking_System_2.Models;
using Microsoft.EntityFrameworkCore; 
namespace Hotel_Booking_System_2.Db
{
    public class HotelBookingContext : DbContext
    {
        //public DbSet<Customers> Customers { get; set; }
        //public DbSet<Staffs> Staffs { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public HotelBookingContext(DbContextOptions<HotelBookingContext> options)
            : base(options)
        {

        }
    }
}
