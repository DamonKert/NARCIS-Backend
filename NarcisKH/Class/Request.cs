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
        public int? Age { get; set; }
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
    public class CreateClothRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal? Discount { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<SizeAndQuantity>? sizeAndQuantities { get; set; }
    }
    public class UpdateClothRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal? Discount { get; set; }
        public int? CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<SizeAndQuantity>? sizeAndQuantities { get; set; }
        public List<string>? RemainingImages { get; set; } //imagePaths
    }
    public class  SizeAndQuantity 
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
    }
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    public class UpdateCategoryRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }

}
