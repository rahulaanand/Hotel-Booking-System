using Hotel_Booking_System_2.Models;
using Hotel_Booking_System_2.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hotel_Booking_System_2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        // GET: api/Hotels
        [HttpGet]
        public ActionResult<IEnumerable<Hotels>> GetHotels()
        {
            var hotels = _hotelRepository.GetAllHotels();
            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public ActionResult<Hotels> GetHotel(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
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
                _hotelRepository.AddHotel(hotel);
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
                _hotelRepository.UpdateHotel(hotel);
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
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            try
            {
                _hotelRepository.DeleteHotel(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
