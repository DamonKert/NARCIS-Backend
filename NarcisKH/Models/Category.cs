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

        public int? ParentId { get; set; }

        [JsonIgnore]
        [ForeignKey("ParentId")]
        public Category Parent { get; set; } = default!;

        public List<Category> Children { get; set; } = new List<Category>();
    }
}
