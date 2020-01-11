using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FEWebsite.API.Models.AbstractModels;
using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime AccountCreated { get; set; }

        public DateTime LastLogin { get; set; }

        public Gender Gender { get; set; }

        [Column("Gender")]
        public string GenderId { get; set; }

        public ICollection<UserGame> FavoriteGames { get; set; }

        public ICollection<UserGameGenre> FavoriteGenres { get; set; }

        public ICollection<Photo> Photos { get; set; }

        [Column("AboutMe")]
        public override string Description { get; set; }

        [Column("Alias")]
        public override string Name { get; set; }
    }
}
