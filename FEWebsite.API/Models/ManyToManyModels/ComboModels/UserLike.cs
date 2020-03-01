namespace FEWebsite.API.Models.ManyToManyModels.ComboModels
{
    public class UserLike
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }
    }
}
