using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data.BaseServices;
using System.Threading.Tasks;
using FEWebsite.API.Models;

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
        public async Task<IActionResult> Register(string username, string password) {
            username = username.ToLower();

            if (await this.Repo.UserExists(username).ConfigureAwait(false))
            {
                return this.BadRequest("Username already exists.");
            }

            var newUser = new User() {
                Username = username,
            };

            var createdUser = await this.Repo.Register(newUser, password);

            return this.StatusCode(201);
        }
    }
}
