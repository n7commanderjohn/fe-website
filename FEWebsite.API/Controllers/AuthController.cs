using System;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data.BaseServices;
using System.Threading.Tasks;
using FEWebsite.API.Models;
using FEWebsite.API.DTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace FEWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepositoryService Repo { get; }
        public IConfiguration Config { get; }

        public AuthController(IAuthRepositoryService repo, IConfiguration config)
        {
            this.Repo = repo;
            this.Config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await this.Repo.UserExists(userForRegisterDto.Username).ConfigureAwait(false))
            {
                return this.BadRequest("Username already exists.");
            }

            var newUser = new User() {
                Username = userForRegisterDto.Username,
            };

            var createdUser = await this.Repo.Register(newUser, userForRegisterDto.Password).ConfigureAwait(false);

            return this.StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            throw new Exception("badbad");

            var userFromRepo = await Repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password).ConfigureAwait(false);

            if (userFromRepo == null)
            {
                return this.Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
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
