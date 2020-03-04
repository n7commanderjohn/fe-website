using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Helpers;
using FEWebsite.API.Models.ManyToManyModels.ComboModels;
using Microsoft.EntityFrameworkCore;

namespace FEWebsite.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public MessageController(IUserService userService, IMapper mapper)
        {
            this.UserService = userService;
            this.Mapper = mapper;
        }

        [HttpGet("{messageId}", Name = "GetUserMessage")]
        public async Task<IActionResult> GetUserMessage(int userId, int messageId)
        {
            var unauthorization = this.CheckIfUserIsAuthorized(userId, "You aren't authorized to view this message.");
            if (unauthorization != null) {
                return unauthorization;
            }

            var userMessage = await this.UserService.GetMessage(messageId).ConfigureAwait(false);

            if (userMessage == null)
            {
                return this.NotFound(new StatusCodeResultReturnObject(this.NotFound(),
                    "No message found."));
            }

            return this.Ok(userMessage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserMessage(int userId, UserMessageCreationDto userMessageCreationDto)
        {
            var unauthorization = this.CheckIfUserIsAuthorized(userId, "You aren't authorized to send this message.");
            if (unauthorization != null) {
                return unauthorization;
            }

            userMessageCreationDto.SenderId = userId;

            var recipient = await this.UserService.GetUser(userMessageCreationDto.RecipientId).ConfigureAwait(false);
            if (recipient == null) {
                return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                    "The recipent could not be found."));
            }

            var outgoingMessage = this.Mapper.Map<UserMessage>(userMessageCreationDto);
            this.UserService.Add(outgoingMessage);

            if (await this.UserService.SaveAll().ConfigureAwait(false))
            {
                var returnMessageObj = this.Mapper.Map<UserMessageCreationDto>(outgoingMessage);
                return this.CreatedAtRoute("GetUserMessage", new {userId, messageId = outgoingMessage.Id},
                    returnMessageObj);
            }

            throw new DbUpdateException("The User Message failed to be created.");
        }
    }
}
