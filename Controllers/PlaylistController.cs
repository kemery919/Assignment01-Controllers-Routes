using Emery_ChinookEndpoints.Data;
using Emery_ChinookEndpoints.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emery_ChinookEndpoints.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class PlaylistController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public PlaylistController(ApplicationDbContext context) {
      _context = context;
    }

    // Get All Playlists
    [HttpGet("")] 
    public async Task<ActionResult<List<Playlist>>> GetAllPlaylists() {
      var playlists = await _context.Playlist.ToListAsync();

      return Ok(playlists);
    }

    // Get Playlist by ID
    [HttpGet("{playlistId:int}")] 
    public async Task<ActionResult<Playlist>> GetPlaylistById(int playlistId) {
      var playlist = await _context.Playlist.SingleOrDefaultAsync(pl => pl.PlaylistId == playlistId);

      if (playlist == null) {
        return NotFound($"No playlist found with the ID: {playlistId}");
      }

      return Ok(playlist);
    }

    // Delete Playlist
    [HttpDelete("{playlistId:int}")] 
    public async Task<ActionResult> DeletePlaylist(int playlistId) {
      var playlist = await _context.Playlist.SingleOrDefaultAsync(pl => pl.PlaylistId == playlistId);

      if (playlist == null) {
        return NotFound($"No playlist found with the ID: {playlistId}");
      }

      _context.Playlist.Remove(playlist);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpGet("top-expensive")]
    public async Task<ActionResult<List<Playlist>>> GetTopExpensivePlaylists(int numTopExpensive) {
      var playlists = await _context.Playlist
        .Select(pl => new { PlaylistId = pl.PlaylistId, 
                            Name = pl.Name,
                            SongCount = pl.Tracks.Count, 
                            PlaylistExpense = (double)pl.Tracks.Sum(t => t.UnitPrice) })
        .OrderByDescending(pl => pl.PlaylistExpense)
        .Take(numTopExpensive)
        .ToListAsync();

      return Ok(playlists);
    }
    
  }
}