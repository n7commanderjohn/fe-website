using Microsoft.EntityFrameworkCore;
using FEWebsite.API.Models;
using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<GameGenre> GameGenres { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<UserGame> UserGames { get; set; }

        public DbSet<UserGameGenre> UserGameGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGame>().HasKey(ug => new { ug.UserId, ug.GameId });
            modelBuilder.Entity<UserGameGenre>().HasKey(ug => new { ug.UserId, ug.GameGenreId });
        }
    }
}
