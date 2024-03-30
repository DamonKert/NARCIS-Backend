using NarcisKH.Models;

namespace NarcisKH.Class.Customer.Cart
{
	public class GetCartResponse
	{
		public List<ClothDTO> Clothes { get; set; } = new List<ClothDTO>();
	}

	public class ClothDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Model? Model { get; set; } = default!;
		public decimal Price { get; set; }
		public decimal Discount { get; set; } = 0;
		public string? Code { get; set; } = default!;
		public Category Category { get; set; } = default!;
		public List<string> ImagePaths { get; set; } = new List<string>();
		public List<Size>? Sizes { get; set; } = new List<Size>();
		public Detail Detail { get; set; } = default!;
	}
	public class Detail
	{
		public Size Size { get; set; } = new Size();
		public int Quantity { get; set; }
	}
}
