namespace FEWebsite.API.Core.Models.AbstractModels
{
    abstract public class BaseModel
    {
        public int Id { get; set; }

        public virtual string Description { get; set; }

        public virtual string Name { get; set; }
    }
}
