using Microsoft.EntityFrameworkCore;
using SFF.Models;

namespace SFF.Context
{
    public class myDbContext : DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Trivia> Trivias { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Ratings)
                        .WithOne(r => r.Movie);

            // one to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Trivias)
                        .WithOne(t => t.Movie);

            // many to many
            // composite key
            modelBuilder.Entity<Rental>()
                        .HasKey(r => new { r.MovieId, r.StudioId });
        }
    }
}