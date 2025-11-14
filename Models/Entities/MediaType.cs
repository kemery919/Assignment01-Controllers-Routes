using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Emery_ChinookEndpoints.Models.Entities {

    public class MediaType {
        [Key]
        public int MediaTypeId { get; set; }

        [MaxLength(120)]
        public string? Name { get; set; } 
    }
}