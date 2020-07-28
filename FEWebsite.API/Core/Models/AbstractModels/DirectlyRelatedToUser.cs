namespace FEWebsite.API.Core.Models.AbstractModels
{
    abstract public class DirectlyRelatedToUser : BaseModel
    {
        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
