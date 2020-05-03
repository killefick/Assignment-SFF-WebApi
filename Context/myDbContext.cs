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


        // Specify DbSet properties etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                        .HasOne(r => r.Movie)
                        .WithMany(r => r.Rental)
                        .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Rental>()
                        .HasOne(t => t.Studio)
                        .WithMany(t => t.Rental)
                        .HasForeignKey(t => t.StudioId);

            modelBuilder.Entity<Movie>()
                        .HasOne<Trivia>()
                        .WithMany()
                        .HasForeignKey(m => m.TriviaId);
        }
    }
}