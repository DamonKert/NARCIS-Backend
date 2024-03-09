using NarcisKH.Class;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NarcisKH.Models
{
    public class Order
    {
        public Order()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            clothes = new List<ClothSizeQuantity>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public CityProvince CityProvince { get; set; }
        public Status Status { get; set; }
        public User Employee { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ClothSizeQuantity> clothes { get; set; } = new List<ClothSizeQuantity>();
    }
    public class ClothSizeQuantity
    {
        public int Id { get; set; }
        public Cloth Cloth { get; set; } = default!;
        public string Size { get; set; } = default!;
        public int Quantity { get; set; }
    }

}
