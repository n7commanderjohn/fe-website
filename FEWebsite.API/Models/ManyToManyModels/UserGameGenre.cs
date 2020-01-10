using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models.ManyToManyModels
{
    public class UserGameGenre : ManyToManyWithUser
    {
        public GameGenre GameGenre { get; set; }

        public int GameGenreId { get; set; }
    }
}
