using System.Collections.Generic;
using System.Threading.Tasks;

using FEWebsite.API.Models.ManyToManyModels;

namespace FEWebsite.API.Interfaces
{
    public interface IGameGenreService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<IEnumerable<GameGenre>> GetGameGenres();

        Task<GameGenre> GetGameGenre(int gameGenreId);
    }
}
