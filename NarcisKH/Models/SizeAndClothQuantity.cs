namespace NarcisKH.Models
{
    public class SizeAndClothQuantity
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int ClothId { get; set; }
        public int Quantity { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Size Size { get; set; } = default!;
        [System.Text.Json.Serialization.JsonIgnore]
        public Cloth Cloth { get; set; } = default!;
    }
}
