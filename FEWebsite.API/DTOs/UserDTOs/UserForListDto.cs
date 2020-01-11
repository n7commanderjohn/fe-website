using System;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserForListDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }

        public DateTime AccountCreated { get; set; }

        public DateTime LastLogin { get; set; }

        public string Gender { get; set; }

        public string Alias { get; set; }

        public string PhotoUrl { get; set; }
    }
}
