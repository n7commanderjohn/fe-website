using System.Collections.Generic;
using FEWebsite.API.Models.AbstractModels;
using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.Models
{
    public class Game : BaseModel
    {
        public ICollection<UserGame> UserGames { get; set; }
    }
}
