namespace FEWebsite.API.Models.AbstractModels
{
    abstract public class ManyToManyWithUser
    {
        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
