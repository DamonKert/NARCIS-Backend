using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace NarcisKH.Models
{
    public class DeliveryStatus
    {
        public DeliveryStatus()
        {
            Status = new List<Status>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? DeliveryProof { get; set; } //CDN path
        [JsonIgnore]
        public ICollection<Status> Status { get; set; }

    }
}
