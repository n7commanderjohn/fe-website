using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FEWebsite.API.Data.BaseServices;
using AutoMapper;
using FEWebsite.API.DTOs.UserDTOs;
using System.Collections.Generic;
using System;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService UserService { get; }

        private IMapper Mapper { get; }

        public UsersController(IUsersService userService, IMapper mapper)
        {
            this.UserService = userService;
            this.Mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        public async Task<OkObjectResult> GetUsers()
        {
            var users = await this.UserService
                .GetUsers()
                .ConfigureAwait(false);

            var usersDto = this.Mapper.Map<IEnumerable<UserForListDto>>(users);

            return this.Ok(usersDto);
        }

        // GET api/users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<OkObjectResult> GetUser(int id)
        {
            var user = await this.UserService
                .GetUser(id)
                .ConfigureAwait(false);

            var userDto = this.Mapper.Map<UserForDetailedDto>(user);

            return this.Ok(userDto);
        }

        // GET api/users
        [AllowAnonymous]
        [HttpGet("genders")]
        public async Task<OkObjectResult> GetGenders()
        {
            var genders = await this.UserService
                .GetGenders()
                .ConfigureAwait(false);

            return this.Ok(genders);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return this.Unauthorized();
            }

            var currentUser = await this.UserService.GetUser(id).ConfigureAwait(false);

            this.Mapper.Map(userForUpdateDto, currentUser);

            if (await this.UserService.SaveAll().ConfigureAwait(false)) {
                return this.NoContent();
            }

            throw new Exception($"Update user {id} failed on save.");
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
