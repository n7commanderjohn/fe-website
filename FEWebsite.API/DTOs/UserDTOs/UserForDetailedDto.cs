using System;
using System.Collections.Generic;
using FEWebsite.API.Models;
using FEWebsite.API.Models.ManyToManyModels;

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

        public ICollection<UserGame> FavoriteGames { get; set; }

        public ICollection<UserGameGenre> FavoriteGenres { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
