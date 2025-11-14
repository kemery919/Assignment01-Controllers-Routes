namespace Emery_ChinookEndpoints.Models.Dtos;

public class AlbumDto {
  public int AlbumId { get; set; } = default!;
  public string Title { get; set; } = default!;
  public int ArtistId { get; set; } = default!;
}

public class AlbumNameDto {
  public string Title { get; set; } = default!;
}

public class AlbumWithArtistAndTracksDto : AlbumDto {
  public virtual ArtistDto Artist { get; set; } = default!;
  public virtual ICollection<TrackDto> Tracks { get; set; } = new List<TrackDto>();
}

// Not Inheriting from the AlbumDto class so I can have the properties appear in the order I want
public class AlbumWithArtistAndTrackNamesDto {
  public int AlbumId { get; set; } = default!;
  public string Title { get; set; } = default!;
  public int ArtistId { get; set; } = default!;
  public virtual ArtistNameDto Artist { get; set; } = default!;
  public virtual ICollection<TrackNameDto> Tracks { get; set; } = new List<TrackNameDto>();
}