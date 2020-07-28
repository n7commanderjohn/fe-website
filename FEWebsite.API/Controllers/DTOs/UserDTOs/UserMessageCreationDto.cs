using System;

namespace FEWebsite.API.Controllers.DTOs.UserDTOs
{
    public class UserMessageCreationDto
    {
        public int SenderId { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string SenderName { get; set; }
        public int RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }

        public UserMessageCreationDto()
        {
            this.MessageSent = DateTime.Now;
        }
    }
}
