using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models.ManyToManyModels
{
    public class UserGame : ManyToManyWithUser
    {
        public Game Game { get; set; }

        public int GameId { get; set; }
    }
}
