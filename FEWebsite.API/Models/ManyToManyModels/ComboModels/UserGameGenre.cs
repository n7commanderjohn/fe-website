using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models.ManyToManyModels.ComboModels
{
    public class UserGameGenre : ManyToManyWithUser
    {
        public virtual GameGenre GameGenre { get; set; }

        public int GameGenreId { get; set; }
    }
}
