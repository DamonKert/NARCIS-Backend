using System.Text.Json.Serialization;

namespace NarcisKH.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            Status = new HashSet<Status>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Status> Status { get; set; }
    }
}
