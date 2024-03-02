namespace NarcisKH.Models
{
    public class Cloth
    {
        public Cloth() 
        { 
            Sizes = new List<Size>();
            SizeAndClothQuantities = new List<SizeAndClothQuantity>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public int CategoryId { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();
        public Category Category { get; set; } = default!;
        public virtual ICollection<Size>? Sizes { get; set; } = new List<Size>();
        public virtual ICollection<SizeAndClothQuantity>? SizeAndClothQuantities { get; set; } = new List<SizeAndClothQuantity>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
