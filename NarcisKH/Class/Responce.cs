using NarcisKH.Models;

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
        public Category Category { get; set; } = default!;
        public List<string> ImagePaths { get; set; } = new List<string>();
        public decimal Discount { get; set; } = 0;
        public virtual ICollection<SizeAndQuantityDTO> Sizes { get; set; } = new List<SizeAndQuantityDTO>();
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
}
