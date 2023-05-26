using Hotel_Booking_System_2.Db;
using Hotel_Booking_System_2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_Booking_System_2.Repo
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingContext _context;

        public HotelRepository(HotelBookingContext context)
        {
            _context = context;
        }

        public IEnumerable<Hotels> GetAllHotels()
        {
            return _context.Hotels.ToList();
        }

        public Hotels GetHotelById(int hotelId)
        {
            return _context.Hotels.Find(hotelId);
        }

        public void AddHotel(Hotels hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void UpdateHotel(Hotels hotel)
        {
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
        }

        public void DeleteHotel(int hotelId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Hotels> GetLocation(string location)
        {
            return _context.Hotels.Where(e => e.HotelLocation == location).ToList();
        }
        public int GetAvailableRoomCount(string hotelname)
        {
            var hotel = _context.Hotels.Include(f => f.Reservations).FirstOrDefault(f => f.HotelName == hotelname);

            if (hotel == null)
                return 0;

            int totalRooms = hotel.RoomAvailability;
            int bookedRooms = hotel.Reservations.Count();
            int availableRooms = totalRooms - bookedRooms;

            return availableRooms >= 0 ? availableRooms : 0;

        }
        public IEnumerable<Hotels> GetPrice(int price)
        {
            return _context.Hotels.Where(e => e.Price <= price).ToList();
        }

        public IEnumerable<Hotels> GetAmenities(string amenities)
        {
            return _context.Hotels.Where(e => e.Amenities == amenities).ToList();
        }

    }
}
