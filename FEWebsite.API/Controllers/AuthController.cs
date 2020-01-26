using System;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data.BaseServices;
using System.Threading.Tasks;
using FEWebsite.API.Models;
using FEWebsite.API.DTOs.UserDTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace FEWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService { get; }
        public IConfiguration Config { get; }

        public AuthController(IAuthService authService, IConfiguration config)
        {
            this.AuthService = authService;
            this.Config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await this.AuthService.UserExists(userForRegisterDto.Username).ConfigureAwait(false))
            {
                return this.BadRequest("Username already exists.");
            }

            var newUser = new User() {
                Username = userForRegisterDto.Username,
            };

            var createdUser = await this.AuthService.Register(newUser, userForRegisterDto.Password).ConfigureAwait(false);

            if (createdUser != null)
            {
                return this.Created($"api/users/{createdUser.Id}", createdUser);
            }
            else {
                return this.BadRequest("Registration failed when creating the user.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var authenticatedUser = await this.AuthService
                .Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password)
                .ConfigureAwait(false);

            if (authenticatedUser == null)
            {
                return this.Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, authenticatedUser.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return this.Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
