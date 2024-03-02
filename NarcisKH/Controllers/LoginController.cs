using Microsoft.AspNetCore.Mvc;
using NarcisKH.Class;
using NarcisKH.Data;
using NarcisKH.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Telegram.Bot;
using System.Data.Entity;
namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly NarcisKHContext _context;
        public LoginController(IConfiguration config, NarcisKHContext context)
        {
            _config = config;
            _context = context;
        }
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            BotClass bot = new BotClass(new TelegramBotClient("6441721739:AAFvyv5IMs37Oz0Vanqf2UVEeIIaQ3lFlcI"), _context);
            bot.SendMessage();
            bot.StartReceiever();
            return response;
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"],
                             _config["JwtSettings:Audience"],
                                          null,
                                                       expires: DateTime.Now.AddMinutes(120),
                                                                    signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private User AuthenticateUser(User login)
        {
           var User =_context.Users.Include(x => x.Role).FirstOrDefault(x => x.Username.ToLower() == login.Username.ToLower() && x.Password == login.Password);
            return User;
        }
        private LoginResponse Generate(User? user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"],
                _config["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(150),
                signingCredentials: credentials);
            var response = new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Error = "",
                Status = 200,

            };
            return response;
        }

    }
}
