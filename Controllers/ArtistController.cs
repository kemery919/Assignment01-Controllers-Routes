using Emery_ChinookEndpoints.Data;
using Emery_ChinookEndpoints.Models.Entities;
using Emery_ChinookEndpoints.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emery_ChinookEndpoints.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ArtistController : Controller {
    private readonly ApplicationDbContext _context;

    public ArtistController(ApplicationDbContext context) {
      _context = context;
    }

    // Get All Artists
    [HttpGet("")]
    public async Task<ActionResult<List<Artist>>> GetAllArtists() {
      var artists = await _context.Artist
        .Include(ar => ar.Albums)
        .ToListAsync();

      List<ArtistWithAlbumsDto> artistDtos = new List<ArtistWithAlbumsDto>();
      foreach (Artist artist in artists) {
        artistDtos.Add(new ArtistWithAlbumsDto {
          ArtistId = artist.ArtistId,
          Name = artist.Name,
          AlbumCount = artist.Albums.Count,
          Albums = artist.Albums.Select(al => new AlbumNameDto {
            Title = al.Title
          }).ToList()
        });
      } 

      return Ok(artistDtos);
    }

    // Get Artist by ID
    [HttpGet("{artistId:int}")] 
    public async Task<ActionResult<Artist>> GetArtistById(int artistId) {
      if (artistId == null) {
        return NotFound($"No artist found with the ID: {artistId}");
      }
      
      var artist = await _context.Artist
        .Include(ar => ar.Albums)
        .Select(ar => new ArtistWithAlbumsDto {
          ArtistId = ar.ArtistId,
          Name = ar.Name,
          AlbumCount = ar.Albums.Count,
          Albums = ar.Albums.Select(al => new AlbumNameDto {
            Title = al.Title
          }).ToList()
        })
        .SingleOrDefaultAsync(ar => ar.ArtistId == artistId);

      

      return Ok(artist);
    }

    // Delete Artist
    [HttpDelete("{artistId:int}")] 
    public async Task<ActionResult> DeleteArtist(int artistId) {
      var artist = await _context.Artist.SingleOrDefaultAsync(ar => ar.ArtistId == artistId);

      if (artist == null) {
        return NotFound($"No artist found with the ID: {artistId}");
      }

      _context.Artist.Remove(artist);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    // Get Artist Stats
    [HttpGet("stats")] 
    public async Task<ActionResult<List<Artist>>> GetArtistStats(int numAlbumsFromArtist) {
      if (numAlbumsFromArtist <= 0) {
        return BadRequest("Number of albums must be greater than 0");
      }
      
      var artists = await _context.Artist
        .Include(ar => ar.Albums)
        .ToListAsync();

      List<ArtistWithAlbumsDto> artistDtos = new List<ArtistWithAlbumsDto>();
      foreach (Artist artist in artists) {
        artistDtos.Add(new ArtistWithAlbumsDto {
          ArtistId = artist.ArtistId,
          Name = artist.Name,
          AlbumCount = artist.Albums.Count,
          Albums = artist.Albums.Select(al => new AlbumNameDto {
            Title = al.Title
          }).ToList()
        });
      } 

      List<ArtistWithAlbumsDto> trimmedArtists = artistDtos
                  .OrderByDescending(ar => ar.AlbumCount)
                  .Take(numAlbumsFromArtist)
                  .ToList();
      
      return Ok(trimmedArtists); 
    }
    
  }
}