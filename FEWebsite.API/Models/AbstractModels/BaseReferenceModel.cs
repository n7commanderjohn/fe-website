namespace FEWebsite.API.Models.AbstractModels
{
    abstract public class BaseReferenceModel
    {
        public virtual string Id { get; set; }

        public virtual string Description { get; set; }
    }
}
