using System.Collections.Generic;
using System.Threading.Tasks;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FEWebsite.API.Data.DerivedServices
{
    public class GamesService : BaseService, IGamesService
    {
        public GamesService(DataContext context)
        {
            Context = context;
        }

        private DataContext Context { get; }

        public void Add<T>(T entity) where T : class
        {
            this.Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.Context.Remove(entity);
        }

        public async Task<Game> GetGame(int gameId)
        {
            var game = await this.Context.Games
                .FirstOrDefaultAsync(u => u.Id == gameId)
                .ConfigureAwait(false);

            return game;
        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            var games = await this.Context.Games
                .ToListAsync()
                .ConfigureAwait(false);

            return games;
        }

        public async Task<bool> SaveAll()
        {
            return await this.Context
                .SaveChangesAsync()
                .ConfigureAwait(false) > 0;
        }
    }
}
