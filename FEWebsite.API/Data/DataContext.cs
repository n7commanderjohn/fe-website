using Microsoft.EntityFrameworkCore;
using FEWebsite.API.Models;

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
    }
}
