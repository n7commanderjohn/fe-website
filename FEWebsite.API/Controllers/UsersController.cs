using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FEWebsite.API.Data.BaseServices;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserInfoRepository RepoUsers { get; }

        public UsersController(IUserInfoRepository repo)
        {
            this.RepoUsers = repo;
        }

        // GET api/users
        [HttpGet]
        public async Task<OkObjectResult> GetUsers()
        {
            var users = await this.RepoUsers
                .GetUsers()
                .ConfigureAwait(false);

            return this.Ok(users);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetUser(int id)
        {
            var game = await this.RepoUsers
                .GetUser(id)
                .ConfigureAwait(false);

            return this.Ok(game);
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
