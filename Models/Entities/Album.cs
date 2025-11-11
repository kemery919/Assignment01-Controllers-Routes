using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ChinookDtoExample.Models.Entities {
    [DebuggerDisplay("{Title} (AlbumId = {AlbumId})")]
    public class Album {
        [Key]
        public int AlbumId { get; set; }

        [Required, MaxLength(160)]
        public string Title { get; set; } = default!;

        [ForeignKey("Artist")]
        public int ArtistId { get; set; } = default!;

        public virtual Artist? Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}