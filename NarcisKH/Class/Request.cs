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
    }
}
