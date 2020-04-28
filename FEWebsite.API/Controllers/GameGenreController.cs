using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using FEWebsite.API.Core.Interfaces;
using FEWebsite.API.Controllers.DTOs.GameGenreDTOs;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameGenreController : ControllerBase
    {
        private IGameGenreService GameGenreService { get; }

        private IMapper Mapper { get; }

        public GameGenreController(IGameGenreService GameGenreService, IMapper mapper)
        {
            this.GameGenreService = GameGenreService;
            this.Mapper = mapper;
        }

        // GET api/GameGenres
        [AllowAnonymous]
        [HttpGet]
        public async Task<OkObjectResult> GetGameGenres()
        {
            var GameGenres = await this.GameGenreService
                .GetGameGenres()
                .ConfigureAwait(false);

            var GameGenresDto = this.Mapper.Map<IEnumerable<GameGenreForDetailedDto>>(GameGenres);

            return this.Ok(GameGenresDto);
        }

        // GET api/GameGenres/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<OkObjectResult> GetGameGenre(int id)
        {
            var gameGenre = await this.GameGenreService
                .GetGameGenre(id)
                .ConfigureAwait(false);

            var gameGenreDto = this.Mapper.Map<GameGenreForDetailedDto>(gameGenre);

            return this.Ok(gameGenreDto);
        }
    }
}
