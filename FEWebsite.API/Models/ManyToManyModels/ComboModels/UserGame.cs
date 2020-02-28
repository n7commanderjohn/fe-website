using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models.ManyToManyModels.ComboModels
{
    public class UserGame : ManyToManyWithUser
    {
        public Game Game { get; set; }

        public int GameId { get; set; }
    }
}
