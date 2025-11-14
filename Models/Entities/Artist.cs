using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Emery_ChinookEndpoints.Models.Entities {

    public class Artist {
        [Key]
        public int ArtistId { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = default!;
        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}