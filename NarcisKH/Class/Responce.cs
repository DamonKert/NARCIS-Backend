using NarcisKH.Models;
using System.Text.Json.Serialization;

namespace NarcisKH.Class
{
    public class Responce
    {
    }
    public class ClothDTO()
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string Code { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public List<string> ImagePaths { get; set; } = new List<string>();
        public decimal Discount { get; set; } = 0;
        public List<Size> Sizes { get; set; } = new List<Size>();
        public Model Model { get; set; } = default!;
        [JsonIgnore]
        public virtual ICollection<SizeAndQuantityDTO> Size { get; set; } = new List<SizeAndQuantityDTO>();
    }
    public class SizeAndQuantityDTO()
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
    }
    public class GetClothByIdsRequest
    {
        public int CategoryId { get; set; }
        public Sort? Sort { get; set; }
    }
    public class Sort
    {
        public string By { get; set; }
        public int? Mode { get; set; }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Error { get; set; }
        public int Status { get; set; }
        public string Role { get; set; }
    }
    public class GetCategoriesResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Children { get; set; }
        public Category Parent { get; set; }
    }
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public CityProvince CityProvince { get; set; }
        public StatusDTO Status { get; set; }
        public User Employee { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ClothDTO> clothes { get; set; }
    }
    public class StatusDTO
    {
        public DeliveryStatus DeliveryStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? TransactionProofImage { get; set; }
        public List<string>? DeliveryProofImage { get; set; }

    }
}
