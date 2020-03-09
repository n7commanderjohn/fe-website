using System.Collections.Generic;

using FEWebsite.API.Models.AbstractModels;
using FEWebsite.API.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Models.ManyToManyModels
{
    public class GameGenre : BaseModel
    {
        public ICollection<UserGameGenre> UserGameGenres { get; set; }
    }
}
