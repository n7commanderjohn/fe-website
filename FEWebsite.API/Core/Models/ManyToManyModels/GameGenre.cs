using System.Collections.Generic;

using FEWebsite.API.Core.Models.AbstractModels;
using FEWebsite.API.Core.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Core.Models.ManyToManyModels
{
    public class GameGenre : BaseModel
    {
        public virtual ICollection<UserGameGenre> UserGameGenres { get; set; }
    }
}
