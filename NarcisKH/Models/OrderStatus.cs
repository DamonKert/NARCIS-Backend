using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace NarcisKH.Models
{
    public class OrderStatus
    {
        public OrderStatus()
        {
            statuses = new List<Status>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        [JsonIgnore]
        public ICollection<Status> statuses { get; set; }
    }
}
