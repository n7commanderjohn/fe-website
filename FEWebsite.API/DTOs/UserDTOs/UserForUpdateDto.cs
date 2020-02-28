using System;
using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserForUpdateDto
    {
        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string GenderId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string PasswordCurrent { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public Game[] Games { get; set; }

        public GameGenre[] Genres { get; set; }

        public bool IsPasswordNeeded { get; set; }
    }
}
