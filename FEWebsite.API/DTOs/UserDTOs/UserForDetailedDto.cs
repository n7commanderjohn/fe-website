using System;
using System.Collections.Generic;
using FEWebsite.API.Models;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }

        public DateTime AccountCreated { get; set; }

        public DateTime LastLogin { get; set; }

        public string Gender { get; set; }

        public string Alias { get; set; }

        public string PhotoUrl { get; set; }

        public string AboutMe { get; set; }

        public ICollection<Game> FavoriteGames { get; set; }

        public ICollection<GameGenre> FavoriteGenres { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
