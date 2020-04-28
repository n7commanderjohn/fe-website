using FEWebsite.API.Core.Models.AbstractModels;

namespace FEWebsite.API.Core.Models.ManyToManyModels.ComboModels
{
    public class UserGameGenre : ManyToManyWithUser
    {
        public virtual GameGenre GameGenre { get; set; }

        public int GameGenreId { get; set; }
    }
}
