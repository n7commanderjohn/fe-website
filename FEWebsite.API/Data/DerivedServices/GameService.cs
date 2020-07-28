using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using FEWebsite.API.Core.Interfaces;
using FEWebsite.API.Core.Models.ManyToManyModels;

namespace FEWebsite.API.Data.DerivedServices
{
    public class GameService : BaseService, IGameService
    {
        public GameService(DataContext context) : base(context)
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
