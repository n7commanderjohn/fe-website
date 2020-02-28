using FEWebsite.API.Models.AbstractModels;

namespace FEWebsite.API.Models.ManyToManyModels.ComboModels
{
    public class UserLikee : ManyToManyWithUser
    {
        public Likee Likee { get; set; }
        public int LikeeId { get; set; }
    }
}
