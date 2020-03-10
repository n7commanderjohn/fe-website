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
using FEWebsite.API.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }
        private IAuthService AuthService { get; }
        private IConfiguration Config { get; }
        private IMapper Mapper { get; }

        public UserController(IUserService userService, IAuthService authService, IConfiguration config, IMapper mapper)
        {
            this.UserService = userService;
            this.AuthService = authService;
            this.Config = config;
            this.Mapper = mapper;
        }

        // GET api/user
        [HttpGet]
        public async Task<OkObjectResult> GetUsers([FromQuery] UserParams userParams)
        {
            userParams.UserId = this.GetUserIdFromClaim();

            var users = await this.UserService
                .GetUsers(userParams)
                .ConfigureAwait(false);

            var usersDto = this.Mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users);

            return this.Ok(usersDto);
        }

        // GET api/user/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<OkObjectResult> GetUser(int id)
        {
            var user = await this.UserService
                .GetUser(id)
                .ConfigureAwait(false);

            var userDto = this.Mapper.Map<UserForDetailedDto>(user);

            return this.Ok(userDto);
        }

        // GET api/user/5
        [HttpGet("{id}/like")]
        public async Task<OkObjectResult> GetUserLikes(int id)
        {
            var likes = await this.UserService
                .GetLikes(id)
                .ConfigureAwait(false);

            return this.Ok(likes);
        }

        // GET api/user
        [AllowAnonymous]
        [HttpGet("genders")]
        public async Task<OkObjectResult> GetGenders()
        {
            var genders = await this.UserService
                .GetGenders()
                .ConfigureAwait(false);

            return this.Ok(genders);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            var unauthorization = this.CheckIfLoggedInUserIsAuthorized(id, "You aren't authorized to change this password.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            var currentUser = await this.UserService.GetUser(id).ConfigureAwait(false);
            var passwordVerficationPassed = userForUpdateDto.IsPasswordNeeded ?
                this.AuthService.ComparePassword(currentUser, userForUpdateDto.PasswordCurrent) :
                true;
            if (passwordVerficationPassed)
            {
                this.Mapper.Map(userForUpdateDto, currentUser);

                var usernameExistsForAnotherUser = await this.AuthService
                    .UserNameExistsForAnotherUser(currentUser).ConfigureAwait(false);
                var emailExistsForAnotherUser = await this.AuthService
                    .EmailExistsForAnotherUser(currentUser).ConfigureAwait(false);
                if (usernameExistsForAnotherUser)
                {
                    return this.BadRequest(new StatusCodeResultReturnObject(
                        this.BadRequest(), "This username is taken by another user."));
                }
                else if (emailExistsForAnotherUser)
                {
                    return this.BadRequest(new StatusCodeResultReturnObject(
                        this.BadRequest(), "This email is taken by another user."));
                }
                else
                {
                    try
                    {
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
                        return this.BadRequest(new StatusCodeResultReturnObject(
                            this.BadRequest(), "Your information failed to save."));
                    }
                }
            }
            else
            {
                return this.Unauthorized("The provided password does not match.");
            }
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> ToggleUserLikeStatus(int id, int recipientId)
        {
            var unauthorization = this.CheckIfLoggedInUserIsAuthorized(id, "User Id parameter doesn't match the logged in user.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            var like = await this.UserService.GetLike(id, recipientId).ConfigureAwait(false);
            var recipient = await this.UserService.GetUser(recipientId).ConfigureAwait(false);
            string likeStatusMessage;
            if (like != null)
            {
                // var returnObj
                //     = new StatusCodeResultReturnObject(this.BadRequest(), "You already like this user.");
                // return this.BadRequest(returnObj);
                this.UserService.Delete(like); // should untoggle the like by removing it from the Likes table.
                likeStatusMessage = $"You have stopped following {recipient.Name}.";
            }
            else
            {
                like = new UserLike()
                {
                    LikerId = id,
                    LikeeId = recipientId,
                };
                this.UserService.Add(like);
                likeStatusMessage = $"You have started following {recipient.Name}.";
            }

            var userRecordsSaved = await this.UserService.SaveAll().ConfigureAwait(false);
            if (userRecordsSaved)
            {
                return this.Ok(new StatusCodeResultReturnObject(this.Ok(),
                    likeStatusMessage));
            }
            else
            {
                return this.BadRequest(new StatusCodeResultReturnObject(
                    this.BadRequest(), "Failed to update their follow status."));
            }
        }
    }
}
