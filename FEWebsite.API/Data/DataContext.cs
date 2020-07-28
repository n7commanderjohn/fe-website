using Microsoft.EntityFrameworkCore;

using FEWebsite.API.Core.Models;
using FEWebsite.API.Core.Models.ManyToManyModels;
using FEWebsite.API.Core.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

#region Models
#region StandardModels
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; } // should make this many to many eventually for *diversi-REEEEE*
#endregion StandardModels

#region OneToManyModels
        public DbSet<Photo> Photos { get; set; }
#endregion OneToManyModels

#region ManyToManyModels
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
#endregion ManyToManyModels

#region ManyToManyComboModels
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<UserGameGenre> UserGameGenres { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
#endregion ManyToManyComboModels
#endregion Models

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#region SetUpManytoManyRelationships
            modelBuilder.Entity<UserGame>().HasKey(ug => new { ug.UserId, ug.GameId });
            modelBuilder.Entity<UserGameGenre>().HasKey(ug => new { ug.UserId, ug.GameGenreId });

            modelBuilder.Entity<UserLike>().HasKey(ul => new { ul.LikerId, ul.LikeeId });
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(ul => ul.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(ul => ul.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.Sender)
                .WithMany(u => u.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.Recipient)
                .WithMany(u => u.MessagesRecieved)
                .OnDelete(DeleteBehavior.Restrict);
#endregion SetUpManytoManyRelationships
        }
    }
}
