using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data.BaseServices;
using System.Threading.Tasks;
using FEWebsite.API.Models;
using FEWebsite.API.DTOs;
using FEWebsite.API.Data.DerivedServices;

namespace FEWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepositoryService Repo { get; }

        public AuthController(IAuthRepositoryService repo)
        {
            this.Repo = repo;
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
    }
}
