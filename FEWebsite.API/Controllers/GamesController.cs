using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data;

namespace FEWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private DataContext Db { get; set; }

        public GamesController(DataContext db)
        {
            this.Db = db;
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetGames()
        {
            var games = this.Db.Games.ToList();

            return Ok(games);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetGame(int id)
        {
            var game = this.Db.Games.FirstOrDefault(r => r.Id == id);

            return Ok(game);
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
