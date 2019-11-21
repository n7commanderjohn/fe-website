namespace FEWebsite.API.Models.AbstractModels
{
    abstract public class DirectlyRelatedToUser : BaseModel
    {
        public User User { get; set; }

        public int UserId { get; set; }
    }
}
