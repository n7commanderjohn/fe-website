using System;
using System.ComponentModel.DataAnnotations;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        public string Password { get; set; }
    }
}
