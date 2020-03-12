namespace FEWebsite.API.Models.ManyToManyModels.ComboModels
{
    public class UserLike
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public virtual User Liker { get; set; }
        public virtual User Likee { get; set; }
    }
}
