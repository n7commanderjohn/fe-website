using System.Collections.Generic;
using System.Threading.Tasks;

using FEWebsite.API.Core.Models.ManyToManyModels;

namespace FEWebsite.API.Core.Interfaces
{
    public interface IGameService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<IEnumerable<Game>> GetGames();

        Task<Game> GetGame(int gameId);
    }
}
