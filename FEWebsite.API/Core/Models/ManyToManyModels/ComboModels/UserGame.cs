using FEWebsite.API.Core.Models.AbstractModels;

namespace FEWebsite.API.Core.Models.ManyToManyModels.ComboModels
{
    public class UserGame : ManyToManyWithUser
    {
        public virtual Game Game { get; set; }

        public int GameId { get; set; }
    }
}
