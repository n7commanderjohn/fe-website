using System.Collections.Generic;
using FEWebsite.API.Models.AbstractModels;
using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.Models
{
    public class GameGenre : BaseModel
    {
        public ICollection<UserGameGenre> UserGameGenres { get; set; }
    }
}
