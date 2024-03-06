using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NarcisKH.Models
{
    public class Category
    {
        public Category()
        {
            Children = new List<Category>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        [JsonIgnore]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category Parent { get; set; } = default!;

        [JsonIgnore]
        public List<Category> Children { get; set; } = new List<Category>();
    }
}
