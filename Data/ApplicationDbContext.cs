using Microsoft.EntityFrameworkCore;
using Emery_ChinookEndpoints.Models.Entities;

namespace Emery_ChinookEndpoints.Data;

public class ApplicationDbContext : DbContext {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
    }
    public DbSet<Album> Album { get; set; } = default!;
    public DbSet<Artist> Artist { get; set; } = default!;
    public DbSet<Customer> Customer { get; set; } = default!;
    public DbSet<Employee> Employee { get; set; } = default!;
    public DbSet<Genre> Genre { get; set; } = default!;
    public DbSet<Invoice> Invoice { get; set; } = default!;
    public DbSet<InvoiceLine> InvoiceLine { get; set; } = default!;
    public DbSet<MediaType> MediaType { get; set; } = default!;
    public DbSet<Playlist> Playlist { get; set; } = default!;
    public DbSet<Track> Track { get; set; } = default!;
    public DbSet<PlaylistTrack> PlaylistTrack { get; set; } = default!;

    // Added this to modify intial EFCore auto creation

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Playlist>()
            .HasMany(pl => pl.Tracks)
            .WithMany(t => t.Playlists)
            .UsingEntity<PlaylistTrack>();
    }
}