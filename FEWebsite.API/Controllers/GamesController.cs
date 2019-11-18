using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private DataContext Db { get; }

        public GamesController(DataContext db)
        {
            this.Db = db;
        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await this.Db.Games.ToListAsync().ConfigureAwait(false);

            return this.Ok(games);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await this.Db.Games.FirstOrDefaultAsync(r => r.Id == id).ConfigureAwait(false);

            return this.Ok(game);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
