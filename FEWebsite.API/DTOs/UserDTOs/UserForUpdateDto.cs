using System;
using FEWebsite.API.Models;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserForUpdateDto
    {
        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
