using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using FEWebsite.API.Core.Interfaces;
using FEWebsite.API.Controllers.DTOs.GameDTOs;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService GameService { get; }

        private IMapper Mapper { get; }

        public GameController(IGameService gameService, IMapper mapper)
        {
            this.GameService = gameService;
            this.Mapper = mapper;
        }

        // GET api/games
        [AllowAnonymous]
        [HttpGet]
        public async Task<OkObjectResult> GetGames()
        {
            var games = await this.GameService
                .GetGames()
                .ConfigureAwait(false);

            var gamesDto = this.Mapper.Map<IEnumerable<GameForDetailedDto>>(games);

            return this.Ok(gamesDto);
        }

        // GET api/games/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetGame(int id)
        {
            var game = await this.GameService
                .GetGame(id)
                .ConfigureAwait(false);

            var gameDto = this.Mapper.Map<GameForDetailedDto>(game);

            return this.Ok(gameDto);
        }
    }
}
