using Microsoft.EntityFrameworkCore;

namespace ChinookDtoExample.Models.Entities;

[PrimaryKey(nameof(PlaylistId), nameof(TrackId))]
public class PlaylistTrack {
    public int PlaylistId { get; set; }
    public int TrackId { get; set; }
}