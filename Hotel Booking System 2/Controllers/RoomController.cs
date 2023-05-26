using APIcodefirst.Repository;
using Hotel_Booking_System_2.Models;
using Hotel_Booking_System_2.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hotel_Booking_System_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _context;

        public RoomController(IRoomRepository _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rooms>> GetRoom()
        {
            try
            {
                return Ok(_context.GetRoom());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Rooms> GetRoomByid(int id)
        {
            try
            {
                var rooms = _context.GetRoomByid(id);
                if (rooms == null)
                {
                    return NotFound();
                }
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Rooms> Post(Rooms room)
        {
            try
            {
                return Ok(_context.PostRoom(room));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Rooms room)
        {
            try
            {
                _context.PutRoom(room);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            try
            {
                _context.DeleteRoom(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
