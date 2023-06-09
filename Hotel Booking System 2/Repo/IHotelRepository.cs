﻿using Hotel_Booking_System_2.Models;
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
        IEnumerable<Hotels> GetLocation(string location);
        int GetAvailableRoomCount(string hotelname);
        IEnumerable<Hotels> GetAmenities(string amenities);
        IEnumerable<Hotels> GetPrice(int price);
        IEnumerable<Hotels> FilterHotels(string location, int price, string amenities);


    }
}
