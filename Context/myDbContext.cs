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
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Ratings)
                        .WithOne(r => r.Movie)
                        .IsRequired();

            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Trivias)
                        .WithOne(t => t.Movie)
                        .IsRequired();
        }
    }
}