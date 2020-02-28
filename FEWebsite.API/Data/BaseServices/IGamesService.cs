using System.Collections.Generic;
using System.Threading.Tasks;
using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.Data.BaseServices
{
    public interface IGamesService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<IEnumerable<Game>> GetGames();

        Task<Game> GetGame(int gameId);
    }
}
