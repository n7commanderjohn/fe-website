using System;

namespace FEWebsite.API.DTOs.UserDTOs
{
    public class UserMessageCreationDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }

        public UserMessageCreationDto()
        {
            this.MessageSent = DateTime.Now;
        }
    }
}
