using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Helpers;
using FEWebsite.API.Models.ManyToManyModels.ComboModels;

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
            if (unauthorization != null)
            {
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

        [HttpGet]
        public async Task<IActionResult> GetUserMessages(int userId, [FromQuery]MessageParams messageParams)
        {
            var unauthorization = this.CheckIfUserIsAuthorized(userId, "You aren't authorized to view these messages.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            messageParams.UserId = userId;
            var pagedUserMessages = await this.UserService.GetMessagesForUser(messageParams)
                .ConfigureAwait(false);

            var userMessages = this.Mapper.Map<IEnumerable<UserMessageToReturnDto>>(pagedUserMessages);

            Response.AddPagination(pagedUserMessages);

            return this.Ok(userMessages);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetUserMessageThread(int userId, int recipientId)
        {
            var unauthorization = this.CheckIfUserIsAuthorized(userId, "You aren't authorized to view these messages.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            var messageThread = await this.UserService.GetMessageThread(userId, recipientId).ConfigureAwait(false);

            var returnMessageThread = this.Mapper.Map<IEnumerable<UserMessageToReturnDto>>(messageThread);

            return this.Ok(returnMessageThread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserMessage(int userId, UserMessageCreationDto userMessageCreationDto)
        {
            var unauthorization = this.CheckIfUserIsAuthorized(userId, "You aren't authorized to send this message.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            userMessageCreationDto.SenderId = userId;

            var recipient = await this.UserService.GetUser(userMessageCreationDto.RecipientId).ConfigureAwait(false);
            if (recipient == null)
            {
                return this.BadRequest(new StatusCodeResultReturnObject(this.BadRequest(),
                    "The recipent could not be found."));
            }

            var outgoingMessage = this.Mapper.Map<UserMessage>(userMessageCreationDto);
            this.UserService.Add(outgoingMessage);

            if (await this.UserService.SaveAll().ConfigureAwait(false))
            {
                var sender = await this.UserService.GetUser(userMessageCreationDto.SenderId).ConfigureAwait(false);
                if (sender != null)
                {
                    outgoingMessage.Sender = sender;
                }

                var returnMessageObj = this.Mapper.Map<UserMessageToReturnDto>(outgoingMessage);
                return this.CreatedAtRoute("GetUserMessage", new {userId, messageId = outgoingMessage.Id},
                    returnMessageObj);
            }

            throw new DbUpdateException("The User Message failed to be created.");
        }

        [HttpPost("{messageId}")]
        public async Task<IActionResult> DeleteUserMessage(int messageId, int userId)
        {
            var unauthorization = this.CheckIfUserIsAuthorized(userId, "You aren't authorized to delete this message.");
            if (unauthorization != null)
            {
                return unauthorization;
            }

            var messageToDelete = await this.UserService.GetMessage(messageId).ConfigureAwait(false);

            if (messageToDelete.SenderId == userId)
            {
                messageToDelete.SenderDeleted = true;
            }
            if (messageToDelete.RecipientId == userId)
            {
                messageToDelete.RecipientDeleted = true;
            }

            if (messageToDelete.SenderDeleted && messageToDelete.RecipientDeleted)
            {
                this.UserService.Delete(messageToDelete);
            }

            if (await this.UserService.SaveAll().ConfigureAwait(false))
            {
                return this.NoContent();
            }

            throw new DbUpdateException("The User Message failed to be deleted.");
        }
    }
}
