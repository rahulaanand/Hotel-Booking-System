using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_Booking_System_2.Db;
using Hotel_Booking_System_2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Hotel_Booking_System_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly HotelBookingContext _context;

        public StaffsController(HotelBookingContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Staff")]
        // GET: api/Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staffs>>> GetStaffs()
        {
            var staffs = await _context.Staffs.ToListAsync();
            var GetStaff = staffs.Select(s => new Staffs
            {
                StaffId = s.StaffId,
                StaffName = s.StaffName,
                StaffPassword = HashPassword(s.StaffPassword),
                HotelId = s.HotelId
            }).ToList();
            return GetStaff;
        }

        [Authorize(Roles = "Staff")]
        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staffs>> GetStaffs(int id)
        {
          if (_context.Staffs == null)
          {
              return NotFound();
          }
            var staffs = await _context.Staffs.FindAsync(id);

            if (staffs == null)
            {
                return NotFound();
            }

            return staffs;
        }

        [Authorize(Roles = "Staff")]
        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffs(int id, Staffs staffs)
        {
            if (id != staffs.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(staffs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Staffs>> PostStaffs(Staffs staffs)
        {
          if (_context.Staffs == null)
          {
              return Problem("Entity set 'HotelBookingContext.Staffs'  is null.");
          }
            _context.Staffs.Add(staffs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffs", new { id = staffs.StaffId }, staffs);
        }

        [Authorize(Roles = "Staff")]
        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffs(int id)
        {
            if (_context.Staffs == null)
            {
                return NotFound();
            }
            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string HashPassword(string password)
        {
            // Implement password hashing logic or any other modification you require
            // Example: Hash the password using SHA256
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool StaffsExists(int id)
        {
            return (_context.Staffs?.Any(e => e.StaffId == id)).GetValueOrDefault();
        }
    }
}
