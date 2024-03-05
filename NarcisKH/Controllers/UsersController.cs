using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Class;
using NarcisKH.Data;
using NarcisKH.Models;

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NarcisKHContext _context;

        public UsersController(NarcisKHContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.Include(x=>x.Role).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(x=>x.Role).FirstOrDefaultAsync(x=>x.Id == id);

            if (user == null)
            {
               var notFoundResponse = new
               {
                   StatusCode = 404,
                   Message = "User Not Found"
               };
                return NotFound(notFoundResponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "User Found",
                Data = user
            };
            return Ok(successResponse);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody]CreateUserRequest user)
        {
            List<string> errors = new List<string>();
            if (user.Username == null)
            {
                errors.Add("Username field is required");
            }
            if(user.Password  == null)
            {
                errors.Add("Password field is required");
            }
            else
            {
                if(user.Password != user.ConfirmPassword)
                {
                    errors.Add("Password and Confirm Password do not match");
                }
            }
            if (user.ConfirmPassword == null)
            {
                errors.Add("Confirm Password field is required");
            }
            if (user.PhoneNumber == null)
            {
                errors.Add("PhoneNumber field is required");
            }
            else
            {
                if(int.TryParse(user.PhoneNumber, out int result) == false)
                {
                    errors.Add("PhoneNumber must be a number");
                }
            }
            if (user.RoleId == 0 || user.RoleId == null)
            {
                errors.Add("RoleId field is required");
            }
            if(errors.Count > 0)
            {
                return BadRequest(new { StatusCode = 400, Message = "Validation Error", Errors = errors });
            }
            var role = _context.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            if(role == null)
            {
                return BadRequest(new { StatusCode = 400, Message = "Validation Error", Errors = new List<string> { "Role does not exist" } });
            }
            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ChatId = user.ChatId,
                Role = role
            };
            var existedUser = _context.Users.FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower());
            if(existedUser != null)
            {
                return BadRequest(new { StatusCode = 400, Message = "Validation Error", Errors = new List<string> { "Username already exists" } });
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 201,
                Message = "User Created Successfully",
                Data = newUser
            };
            return CreatedAtAction("GetUser", new { id = newUser.Id }, successResponse);
        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
