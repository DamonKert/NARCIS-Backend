using System.Text.Json.Serialization;

namespace NarcisKH.Models
{
    public class Size
    {
        public Size()
        {
			Clothes = new List<Cloth>();
        }

		public int Id { get; set; }
        public string Name { get; set; } = default!;
        [JsonIgnore]
        public virtual ICollection<Cloth>? Clothes { get; set; } = new List<Cloth>();
    }
}
