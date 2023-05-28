using Hotel_Booking_System_2.Models;
using Hotel_Booking_System_2.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hotel_Booking_System_2.Controllers
{
    [Authorize(Roles = "Staff,Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _context;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _context = hotelRepository;
        }

        // GET: api/Hotels
        [HttpGet]
        public ActionResult<IEnumerable<Hotels>> GetHotels()
        {
            var hotels = _context.GetAllHotels();
            return Ok(hotels);

        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public ActionResult<Hotels> GetHotel(int id)
        {
            var hotel = _context.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        // POST: api/Hotels
        [HttpPost]
        public ActionResult<Hotels> CreateHotel(Hotels hotel)
        {
            try
            {
                _context.AddHotel(hotel);
                return CreatedAtAction(nameof(GetHotel), new { id = hotel.HotelId }, hotel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, Hotels hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest("Hotel ID mismatch");
            }

            try
            {
                _context.UpdateHotel(hotel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotel = _context.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            try
            {
                _context.DeleteHotel(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Filtering location
        [HttpGet("/filter/location")]
        public ActionResult<IEnumerable<Hotels>> GetLocation(string location)
        {
            try
            {
                var hotels = _context.GetLocation(location);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Count function with ony their Availability 
        [HttpGet("/count")]
        public ActionResult<int> GetAvailableRoom(string hotelname)
        {
            try
            {
                int availableSeats = _context.GetAvailableRoomCount(hotelname);
                return Ok(availableSeats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Filtering according to their Price
        [HttpGet("/filter/price")]
        public ActionResult<IEnumerable<Hotels>> GetPrice(int price)
        {
            try
            {
                var hotels = _context.GetPrice(price);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Filtering their Amenities
        [HttpGet("/filter/amenities")]
        public ActionResult<IEnumerable<Hotels>> GetAmenities(string amenities)
        {
            try
            {
                var hotels = _context.GetAmenities(amenities);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Filtering all together
        [HttpGet("/filter")]
        public ActionResult<IEnumerable<Hotels>> Filter(string location, int price, string amenities)
        {
            try
            {
                var filteredHotels = _context.FilterHotels(location, price, amenities);
                return Ok(filteredHotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
