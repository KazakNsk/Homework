using System.ComponentModel.DataAnnotations;

namespace wiki.Models
{
    public class Page
    {
        [Key]
        public int id { get; set; }
        [Required][MaxLength(128)]
        public string title { get; set; }
        [Required]
        public string snippet { get; set; }
        public long timestamp { get; set; }
    }
}
