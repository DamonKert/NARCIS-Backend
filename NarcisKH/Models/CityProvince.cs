using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NarcisKH.Models
{
    public class CityProvince
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name_kh { get; set; }
        public string Name_en { get; set; }
        public float DeliveryFee { get; set; }
    }
}
