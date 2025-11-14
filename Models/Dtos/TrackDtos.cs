using Emery_ChinookEndpoints.Models.Entities;

namespace Emery_ChinookEndpoints.Models.Dtos;

public class TrackDto {
  public int TrackId { get; set; } = default!;
  public string Name { get; set; } = default!;
  public int AlbumId { get; set; } = default!;
  public AlbumNameDto Album { get; set; } = default!;
  public int MediaTypeId { get; set; } = default!;
  public MediaTypeDto MediaType { get; set; } = default!;
  public int GenreId { get; set; } = default!;
  public string Composer { get; set; } = default!;
  public int Milliseconds { get; set; } = default!;
  public int Bytes { get; set; } = default!;
  public decimal UnitPrice { get; set; } = default!;
}

public class TrackNameDto {
  public string TrackName { get; set; } = default!;
}