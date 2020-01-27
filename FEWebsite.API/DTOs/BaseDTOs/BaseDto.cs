namespace FEWebsite.API.DTOs.BaseDTOs
{
    abstract public class BaseDto
    {
        public int Id { get; set; }

        public virtual string Description { get; set; }

        public virtual string Name { get; set; }
    }
}
