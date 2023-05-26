using Hotel_Booking_System_2.Models;
using System.Collections.Generic;

namespace Hotel_Booking_System_2.Repo
{
    public interface IHotelRepository
    {
        IEnumerable<Hotels> GetAllHotels();
        Hotels GetHotelById(int hotelId);
        void AddHotel(Hotels hotel);
        void UpdateHotel(Hotels hotel);
        void DeleteHotel(int hotelId);
    }
}
