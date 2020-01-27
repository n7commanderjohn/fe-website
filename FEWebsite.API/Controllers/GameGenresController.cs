﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FEWebsite.API.Data.BaseServices;
using AutoMapper;
using System.Collections.Generic;
using FEWebsite.API.DTOs.GameGenreDTOs;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameGenresController : ControllerBase
    {
        private IGameGenresService GameGenresService { get; }

        private IMapper Mapper { get; }

        public GameGenresController(IGameGenresService GameGenresService, IMapper mapper)
        {
            this.GameGenresService = GameGenresService;
            this.Mapper = mapper;
        }

        // GET api/GameGenres
        [AllowAnonymous]
        [HttpGet]
        public async Task<OkObjectResult> GetGameGenres()
        {
            var GameGenres = await this.GameGenresService
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
            var gameGenre = await this.GameGenresService
                .GetGameGenre(id)
                .ConfigureAwait(false);

            var gameGenreDto = this.Mapper.Map<GameGenreForDetailedDto>(gameGenre);

            return this.Ok(gameGenreDto);
        }

        // POST api/GameGenres
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/GameGenres/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/GameGenres/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
