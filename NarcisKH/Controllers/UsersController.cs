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
            var users = await _context.Users.Include(x=>x.Role).ToListAsync();
            if(users.Count == 0)
            {
                var notFoundresponse = new
                {
                    StatusCode = 404,
                    Message = "No Users Found"
                };
                return NotFound(notFoundresponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Users Found",
                Data = users
            };
            return Ok(successResponse);
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
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(UpdateUserRequest user)
        {
           var existedUser = _context.Users.FirstOrDefault(x => x.Id == user.ID);
            if(existedUser == null)
            {
                return NotFound(new { StatusCode = 404, Message = "User Not Found" });
            }
            if(user.Password != user.ConfirmPassword)
            {
                var errorResponse = new
                {
                    StatusCode = 400,
                    Message = "Password and Confirm Password are not matched"
                };
                return BadRequest(errorResponse);
            }
            var takenUsername = _context.Users.FirstOrDefault(x=>x.Id != user.ID && x.Username == user.Username);
            if(takenUsername != null)
            {
                var takenResponse = new
                {
                    StatusCode = 400,
                    Message = "Username is already taken"
                };
                return BadRequest(takenResponse);
            }
            existedUser.Username = user.Username;
            existedUser.Password = user.Password;
            var role = _context.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            if(role == null)
            {
                var errorResponse = new
                {
                    StatusCode = 400,
                    Message = "Role not found, Please give a valid RoleID"
                };
                return BadRequest(errorResponse);
            }
            existedUser.Role = role;
            if (!int.TryParse(user.PhoneNumber, out _))
            {
                var wrongPhoneNumberResponse = new
                {
                    StatusCode = 400,
                    Message = "Please provide a valid phone number"
                };
                return BadRequest(wrongPhoneNumberResponse);
            }
            existedUser.PhoneNumber = user.PhoneNumber;
            if(user.Email != null)
            {
                existedUser.Email = user.Email;
            }
            if(existedUser.ChatId != null)
            {
                existedUser.ChatId = user.ChatId;
            }
           
            _context.Entry(existedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "User Updated Successfully",
                Data = existedUser
            };
            return Ok(successResponse);
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
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                };
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "User Deleted Successfully"
            };
            return Ok(successResponse);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
