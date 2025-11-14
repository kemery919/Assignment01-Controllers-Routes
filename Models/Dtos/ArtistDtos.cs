namespace Emery_ChinookEndpoints.Models.Dtos;

public class ArtistDto {
  public int ArtistId { get; set; } = default!;
  public string Name { get; set; } = default!;
}

public class ArtistNameDto {
  public string Name { get; set; } = default!;
}

public class ArtistWithAlbumsDto {
  public int ArtistId { get; set; } = default!;
  public string Name { get; set; } = default!;
  public int AlbumCount { get; set; } = default!;
  public List<AlbumNameDto> Albums { get; set; } = default!;
}