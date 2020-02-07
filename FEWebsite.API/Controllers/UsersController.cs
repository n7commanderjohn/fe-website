using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Helpers;
using AutoMapper;

namespace FEWebsite.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService UserService { get; }

        private IMapper Mapper { get; }

        private IAuthService AuthService { get; }

        public UsersController(IUsersService userService, IMapper mapper, IAuthService authService)
        {
            this.UserService = userService;
            this.Mapper = mapper;
            this.AuthService = authService;
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
            var passwordVerficationPassed = userForUpdateDto.IsPasswordNeeded ?
                 await this.AuthService
                    .ComparePassword(userForUpdateDto.Username, userForUpdateDto.PasswordCurrent).ConfigureAwait(false)
                : true;
            if (passwordVerficationPassed)
            {
                if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                    return this.Unauthorized();
                }

                var currentUser = await this.UserService.GetUser(id).ConfigureAwait(false);

                this.Mapper.Map(userForUpdateDto, currentUser);

                var usernameExistsForAnotherUser = await this.AuthService
                    .UserNameExistsForAnotherUser(currentUser).ConfigureAwait(false);
                var emailExistsForAnotherUser = await this.AuthService
                    .EmailExistsForAnotherUser(currentUser).ConfigureAwait(false);
                if (usernameExistsForAnotherUser)
                {
                    var returnObj
                        = new StatusCodeResultReturnObject(this.BadRequest(), "This username is taken by another user.");
                    return this.BadRequest(returnObj);
                }
                else if (emailExistsForAnotherUser)
                {
                    var returnObj
                        = new StatusCodeResultReturnObject(this.BadRequest(), "This email is taken by another user.");
                    return this.BadRequest(returnObj);
                }
                else if (await this.UserService.SaveAll().ConfigureAwait(false))
                {
                    return this.NoContent();
                }
                else
                {
                    var returnObj
                        = new StatusCodeResultReturnObject(this.BadRequest(), "Your information failed to save.");
                    return this.BadRequest(returnObj);
                }
            }
            else
            {
                var returnObj
                    = new StatusCodeResultReturnObject(this.Unauthorized(), "The provided password does not match.");
                return this.BadRequest(returnObj);
            }
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
