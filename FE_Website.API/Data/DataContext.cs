using Microsoft.EntityFrameworkCore;
using FE_Website.API.Models;

namespace FE_Website.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
    }
}
