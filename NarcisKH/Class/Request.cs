using Microsoft.AspNetCore.Http;

namespace NarcisKH.Class
{
   public class LoginRequest
    {
       public string Username { get; set; }
       public string Password { get; set; }
   }
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get;set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string? ChatId { get; set; }
    }
    public class CreateModelRequest
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Bottom { get; set; }
        public string Top { get; set; }
        public IFormFile Profile { get; set; }

    }
    public class UpdateModelRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Bottom { get; set; }
        public string Top { get; set; }
        public IFormFile? Profile { get; set; }
    }
    public class UpdateUserRequest
    {
        public int ID { get; set; } = 0;
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ChatId { get; set; }
        public int RoleId { get; set; }
    }
}
