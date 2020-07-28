using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using FEWebsite.API.Core.Interfaces;
using FEWebsite.API.Core.Models.ManyToManyModels;

namespace FEWebsite.API.Data.DerivedServices
{
    public class GameGenreService : BaseService, IGameGenreService
    {
        public GameGenreService(DataContext context) : base(context)
        {
        }

        public void Add<T>(T entity) where T : class
        {
            this.Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.Context.Remove(entity);
        }

        public async Task<GameGenre> GetGameGenre(int gameGenreId)
        {
            var gameGenre = await this.Context.GameGenres
                .FirstOrDefaultAsync(u => u.Id == gameGenreId)
                .ConfigureAwait(false);

            return gameGenre;
        }

        public async Task<IEnumerable<GameGenre>> GetGameGenres()
        {
            var gameGenres = await this.Context.GameGenres
                .ToListAsync()
                .ConfigureAwait(false);

            return gameGenres;
        }

        public async Task<bool> SaveAll()
        {
            return await this.Context
                .SaveChangesAsync()
                .ConfigureAwait(false) > 0;
        }
    }
}
