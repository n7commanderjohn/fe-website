using System;

using FEWebsite.API.Controllers.DTOs.BaseDTOs;

namespace FEWebsite.API.Controllers.DTOs.UserDTOs
{
    public class UserForListDto : BaseDto
    {
        public string Username { get; set; }

        public int Age { get; set; }

        public DateTime AccountCreated { get; set; }

        public DateTime LastLogin { get; set; }

        public string Gender { get; set; }

        public string PhotoUrl { get; set; }
    }
}
