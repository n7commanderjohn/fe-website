using System.ComponentModel;

namespace FEWebsite.API.Helpers
{
    public class MessageParams
    {
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int UserId { get; set; }
        public MessageContainerArgs MessageContainer { get; set; }
    }

    public enum MessageContainerArgs
    {
        [Description("Unread")]
        Unread = 0,

        [Description("Inbox")]
        Inbox = 1,

        [Description("Outbox")]
        Outbox = 2
    }
}
