using Hotel_Booking_System_2.Db;
using Hotel_Booking_System_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampOnetoManyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly HotelBookingContext _context;

        private const string CustomerRole = "Customer";
        private const string StaffRole = "Staff";


        public TokenController(IConfiguration config, HotelBookingContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Customer")]
        //[Authorize(Roles = CustomerRole)]
        public async Task<IActionResult> Post(Customers _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                         new Claim("CustomerId", user.CustomerId.ToString()),
                         new Claim("Email", user.Email),
                        new Claim("Password",user.Password),
                        new Claim(ClaimTypes.Role, CustomerRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Customers> GetUser(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }


        [HttpPost("Staff")]
        //[Authorize(Roles = StaffRole)]
        public async Task<IActionResult> PostStaff(Staffs staffData)
        {
            if (staffData != null && !string.IsNullOrEmpty(staffData.StaffName) && !string.IsNullOrEmpty(staffData.StaffPassword))
            {
                var staff = await GetStaff(staffData.StaffName, staffData.StaffPassword);

                if (staff != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("StaffId", staff.StaffId.ToString()),
                        new Claim("StaffName", staff.StaffName),
                        new Claim("StaffPassword", staff.StaffPassword),
                        new Claim(ClaimTypes.Role, StaffRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Staffs> GetStaff(string staffName, string staffPassword)
        {
            return await _context.Staffs.FirstOrDefaultAsync(s => s.StaffName == staffName && s.StaffPassword == staffPassword);
        }

    }
}

