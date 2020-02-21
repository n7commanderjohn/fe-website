using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AutoMapper;

using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Helpers;

namespace FEWebsite.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService UserService { get; }
        private IAuthService AuthService { get; }
        private IConfiguration Config { get; }
        private IMapper Mapper { get; }

        public UsersController(IUsersService userService, IAuthService authService, IConfiguration config, IMapper mapper)
        {
            this.UserService = userService;
            this.AuthService = authService;
            this.Config = config;
            this.Mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        public async Task<OkObjectResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = this.GetUserIdFromClaim();
            // var retrievedUser = await this.UserService.GetUser(currentUserId).ConfigureAwait(false);

            userParams.UserId = currentUserId;

            var users = await this.UserService
                .GetUsers(userParams)
                .ConfigureAwait(false);

            var usersDto = this.Mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users);

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
            bool userIdInTokenMatches = id == this.GetUserIdFromClaim();
            if (!userIdInTokenMatches) {
                return this.Unauthorized();
            }

            var currentUser = await this.UserService.GetUser(id).ConfigureAwait(false);
            var passwordVerficationPassed = userForUpdateDto.IsPasswordNeeded ?
                this.AuthService.ComparePassword(currentUser, userForUpdateDto.PasswordCurrent)
                : true;
            if (passwordVerficationPassed)
            {
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
                else
                {
                    try {
                        bool hasUpdatedPassword = userForUpdateDto.Password != null;
                        if (hasUpdatedPassword)
                        {
                            this.AuthService.CreatePasswordHash(currentUser, userForUpdateDto.Password);
                        }
                        var userRecordsSaved = await this.UserService.SaveAll().ConfigureAwait(false);
                        if (userRecordsSaved)
                        {
                            var token = this.AuthService.CreateUserToken(currentUser, this.Config.GetAppSettingsToken());
                            var user = this.Mapper.Map<UserForDetailedDto>(currentUser);
                            return this.Ok(new
                            {
                                token,
                                userAge = user.Age
                            });
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        var returnObj
                            = new StatusCodeResultReturnObject(this.BadRequest(), "Your information failed to save.");
                        return this.BadRequest(returnObj);
                    }
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
