using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using FEWebsite.API.Helpers;
using FEWebsite.API.DTOs.UserDTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace FEWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService { get; }
        public IConfiguration Config { get; }
        public IMapper Mapper { get; }

        public AuthController(IAuthService authService, IConfiguration config, IMapper mapper)
        {
            this.AuthService = authService;
            this.Config = config;
            this.Mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (await this.AuthService.UserNameExists(userForRegisterDto.Username).ConfigureAwait(false))
            {
                return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                    "This username is already in use."));
            }

            if (await this.AuthService.EmailExists(userForRegisterDto.Email).ConfigureAwait(false))
            {
                return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                    "This email is already in use."));
            }

            var newUser = this.Mapper.Map<User>(userForRegisterDto);
            var createdUser = await this.AuthService.Register(newUser, userForRegisterDto.Password).ConfigureAwait(false);

            if (createdUser != null)
            {
                var returnUser = this.Mapper.Map<UserForDetailedDto>(createdUser);
                // return this.Created($"api/users/{createdUser.Id}", createdUser);
                return this.CreatedAtRoute("GetUser", new { controller = "Users", id = returnUser.Id }, returnUser);
            }
            else {
                return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                    "Registration failed when creating the user."));
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
                return this.BadRequest(new StatusCodeResultReturnObject(this.Unauthorized(),
                    "Login failed."));
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

            var user = this.Mapper.Map<UserForLoginDto>(authenticatedUser);

            return this.Ok(new {
                token = tokenHandler.WriteToken(token),
                user
            });
        }
    }
}
