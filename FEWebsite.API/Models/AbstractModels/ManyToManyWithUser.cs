namespace FEWebsite.API.Models.AbstractModels
{
    abstract public class ManyToManyWithUser
    {
        public User User { get; set; }

        public int UserId { get; set; }
    }
}
