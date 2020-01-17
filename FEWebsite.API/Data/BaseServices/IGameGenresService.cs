using System.Collections.Generic;
using System.Threading.Tasks;
using FEWebsite.API.Models;

namespace FEWebsite.API.Data.BaseServices
{
    public interface IGameGenresService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<IEnumerable<GameGenre>> GetGameGenres();

        Task<GameGenre> GetGameGenre(int gameGenreId);
    }
}
