using System;
using System.ComponentModel.DataAnnotations;

namespace Test3.Models
{
    public class Page
    {
        public int id { get; set; }

        [Required]
        [StringLength(128)]
        public string title { get; set; }

        [Required]
        public string snippet { get; set; }

        public long timestamp { get; set; }

    }
}
