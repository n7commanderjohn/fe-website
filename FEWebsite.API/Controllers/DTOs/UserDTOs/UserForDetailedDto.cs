using System;
using System.Collections.Generic;

using FEWebsite.API.Controllers.DTOs.BaseDTOs;
using FEWebsite.API.Controllers.DTOs.GameDTOs;
using FEWebsite.API.Controllers.DTOs.GameGenreDTOs;
using FEWebsite.API.Controllers.DTOs.PhotoDTOs;

namespace FEWebsite.API.Controllers.DTOs.UserDTOs
{
    public class UserForDetailedDto : BaseDto
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime AccountCreated { get; set; }

        public DateTime LastLogin { get; set; }

        public string GenderId { get; set; }

        public string Gender { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<string> ListOfGames { get; set; }

        public ICollection<string> ListOfGenres { get; set; }

        public ICollection<int> ListOfLikees { get; set; }

        public ICollection<GameForDetailedDto> Games { get; set; }

        public ICollection<GameGenreForDetailedDto> Genres { get; set; }

        public ICollection<PhotoForDetailedDto> Photos { get; set; }
    }
}
