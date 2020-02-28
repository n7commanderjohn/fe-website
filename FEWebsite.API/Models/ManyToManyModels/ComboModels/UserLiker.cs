using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models.ManyToManyModels.ComboModels
{
    public class UserLiker : ManyToManyWithUser
    {
        public Liker Liker { get; set; }
        public int LikerId { get; set; }
    }
}
