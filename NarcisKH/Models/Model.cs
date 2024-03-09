using System.Text.Json.Serialization;

namespace NarcisKH.Models
{
    public class Model
    {
        public Model() 
        {
            Clothes = new List<Cloth>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? Age { get; set; }
        public int Height { get; set; }
        public int? Weight { get; set; }
        public string Top { get; set; } 
        public string Bottom { get; set; }
        public string? ProfilePicture { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cloth>? Clothes { get; set; } = new List<Cloth>();
    }
}
