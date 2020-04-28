using System.Collections.Generic;

using FEWebsite.API.Core.Models.AbstractModels;
using FEWebsite.API.Core.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Core.Models.ManyToManyModels
{
    public class Game : BaseModel
    {
        public virtual ICollection<UserGame> UserGames { get; set; }
    }
}
