namespace NarcisKH.Class.Customer.Cart
{
	public class GetCartRequest
	{
		public List<GetCart> Clothes { get; set; } = new List<GetCart>();
	}
	public class GetCart
	{
		public int ClothId { get; set; }
		public int Quantity { get; set; }
		public int SizeId { get; set; }
	}
}
