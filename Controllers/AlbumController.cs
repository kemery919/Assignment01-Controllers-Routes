using Emery_ChinookEndpoints.Data;
using Emery_ChinookEndpoints.Models.Entities;
using Emery_ChinookEndpoints.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emery_ChinookEndpoints.Controllers {

  [Route("api/[controller]")]
  [ApiController]
  public class AlbumController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public AlbumController(ApplicationDbContext context) {
      _context = context;
    }

    // Get All Albums 
    [HttpGet("")]
    public async Task<ActionResult<List<AlbumWithArtistAndTrackNamesDto>>> GetAllAlbums(){
      var albums = await _context.Album
        .Include(al => al.Artist)
        .Include(al => al.Tracks)
        .ToListAsync();

      List<AlbumWithArtistAndTrackNamesDto> albumDtos = new List<AlbumWithArtistAndTrackNamesDto>();
      foreach (Album album in albums) {
        List<TrackNameDto> trackNameDtos = new List<TrackNameDto>();
        foreach (Track track in album.Tracks) {
          trackNameDtos.Add(new TrackNameDto {
            TrackName = track.Name
          });
        }

        albumDtos.Add(new AlbumWithArtistAndTrackNamesDto {
          AlbumId = album.AlbumId,
          Title = album.Title,
          ArtistId = album.ArtistId,
          Artist = new ArtistNameDto {
            Name = album.Artist.Name
          },
          Tracks = trackNameDtos
        });
      }
      
      return Ok(albumDtos);
    }

    // Get Album by ID
    [HttpGet("{albumId:int}")]
    public async Task<ActionResult<Album>> GetAlbumById(int albumId){
      var album = await _context.Album
        .Include(al => al.Artist)
        .Include(al => al.Tracks)
          .ThenInclude(t => t.MediaType)
        .SingleOrDefaultAsync(al => al.AlbumId == albumId);

      if (album == null) {
        return NotFound($"No album found with the ID: {albumId}");
      }

      ArtistDto artistDto = new ArtistDto {
        ArtistId = album.ArtistId,
        Name = album.Artist.Name
      };

      AlbumNameDto albumNameDto = new AlbumNameDto {
        Title = album.Title
      };

      List<TrackDto> trackDtos = new List<TrackDto>();
      foreach (Track track in album.Tracks) {
        
        trackDtos.Add(new TrackDto {
          TrackId = track.TrackId,
          Name = track.Name,
          AlbumId = track.AlbumId,
          Album = albumNameDto,
          MediaTypeId = track.MediaTypeId,
          MediaType = new MediaTypeDto {
            Name = track.MediaType.Name,
          },
          GenreId = track.GenreId,
          Composer = track.Composer,
          Milliseconds = track.Milliseconds,
          Bytes = track.Bytes,
          UnitPrice = track.UnitPrice
        });
      }

      AlbumWithArtistAndTracksDto albumDto = new AlbumWithArtistAndTracksDto {
        AlbumId = album.AlbumId,
        Title = album.Title,
        ArtistId = album.ArtistId,
        Artist = artistDto,
        Tracks = trackDtos
      };
      
      return Ok(albumDto);
    }

    // Delete Album
    [HttpDelete("{albumId:int}")] 
    public async Task<ActionResult> DeleteAlbum(int albumId){
      var album = await _context.Album.SingleOrDefaultAsync(al => al.AlbumId == albumId);

      if (album == null) {
        return NotFound($"No album found with the ID: {albumId}");
      }

      _context.Album.Remove(album);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    // Search by Title OR Artist
    [HttpGet("search")]
    public async Task<ActionResult<List<Album>>> SearchAlbums(string? albumName, string? artist) {
      if (albumName == null && artist == null) {
        return NotFound("No search parameters provided");
      } else if (albumName != null && artist == null) {
        var album = await _context.Album
          .Include(al => al.Artist)
          .Include(al => al.Tracks)
            .ThenInclude(t => t.MediaType)
          .SingleOrDefaultAsync(al => al.Title == albumName);;

        if (album == null) {
          return NotFound($"No album found with the title: {albumName}");
        }

        ArtistDto artistDto = new ArtistDto {
          ArtistId = album.ArtistId,
          Name = album.Artist.Name
        };

        AlbumNameDto albumNameDto = new AlbumNameDto {
          Title = album.Title
        };

        List<TrackDto> trackDtos = new List<TrackDto>();
        foreach (Track track in album.Tracks) {
          
          trackDtos.Add(new TrackDto {
            TrackId = track.TrackId,
            Name = track.Name,
            AlbumId = track.AlbumId,
            Album = albumNameDto,
            MediaTypeId = track.MediaTypeId,
            MediaType = new MediaTypeDto {
              Name = track.MediaType.Name,
            },
            GenreId = track.GenreId,
            Composer = track.Composer,
            Milliseconds = track.Milliseconds,
            Bytes = track.Bytes,
            UnitPrice = track.UnitPrice
          });
        }

        AlbumWithArtistAndTracksDto albumDto = new AlbumWithArtistAndTracksDto {
          AlbumId = album.AlbumId,
          Title = album.Title,
          ArtistId = album.ArtistId,
          Artist = artistDto,
          Tracks = trackDtos
        };
        
        return Ok(albumDto);
      } else if (artist != null && albumName == null) {
        var albums = await _context.Album
          .Include(al => al.Artist)
          .Include(al => al.Tracks)
            .ThenInclude(t => t.MediaType)
          .Where(al => al.Artist.Name.Contains(artist))
          .ToListAsync();

        if (albums.Count == 0) {
          return NotFound($"No artist found with the name: {artist}");
        }

        List<AlbumWithArtistAndTrackNamesDto> albumDtos = new List<AlbumWithArtistAndTrackNamesDto>();
        foreach (Album album in albums) {
          List<TrackNameDto> trackNameDtos = new List<TrackNameDto>();
          foreach (Track track in album.Tracks) {
            trackNameDtos.Add(new TrackNameDto {
              TrackName = track.Name
            });
          }

          albumDtos.Add(new AlbumWithArtistAndTrackNamesDto {
            AlbumId = album.AlbumId,
            Title = album.Title,
            ArtistId = album.ArtistId,
            Artist = new ArtistNameDto {
              Name = album.Artist.Name
            },
            Tracks = trackNameDtos
          });
        }

        return Ok(albumDtos);
      } else {
        // This should only occur when both albumName and artist are provided
        return BadRequest("Only one search parameter can be provided");
      }
    }

    // Get Albums by Artist
    [HttpGet("albums-by-artist")]
    public async Task<ActionResult<List<Album>>> GetAlbumsByArtist(string artistName) {
      var albums = await _context.Album
        .Include(al => al.Artist)
        .Include(al => al.Tracks)
          .ThenInclude(t => t.MediaType)
        .Where(al => al.Artist.Name == artistName)
        .ToListAsync();

      List<AlbumWithArtistAndTrackNamesDto> albumDtos = new List<AlbumWithArtistAndTrackNamesDto>();
      foreach (Album album in albums) {
        List<TrackNameDto> trackNameDtos = new List<TrackNameDto>();
        foreach (Track track in album.Tracks) {
          trackNameDtos.Add(new TrackNameDto {
            TrackName = track.Name
          });
        }

        albumDtos.Add(new AlbumWithArtistAndTrackNamesDto {
          AlbumId = album.AlbumId,
          Title = album.Title,
          ArtistId = album.ArtistId,
          Artist = new ArtistNameDto {
            Name = album.Artist.Name
          },
          Tracks = trackNameDtos
        });
      }

      return Ok(albumDtos);
    }
  
  }
}