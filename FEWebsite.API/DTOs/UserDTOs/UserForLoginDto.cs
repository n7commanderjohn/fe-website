using System.ComponentModel.DataAnnotations;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserForLoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
