using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FEWebsite.API.Data.DerivedServices
{
    public class UserInfoRepository : BaseService, IUserInfoRepository
    {
        public UserInfoRepository(DataContext context)
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

        public async Task<User> GetUser(int userId)
        {
            var user = await this.DefaultUserIncludes()
                .FirstOrDefaultAsync(u => u.Id == userId)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await this.DefaultUserIncludes()
                .ToListAsync()
                .ConfigureAwait(false);

            return users;
        }

        private IQueryable<User> DefaultUserIncludes() {
            return this.Context.Users
                .Include(u => u.Photos)
                .Include(u => u.FavoriteGames)
                .Include(u => u.FavoriteGenres);
        }

        public async Task<bool> SaveAll()
        {
            return await this.Context
                .SaveChangesAsync()
                .ConfigureAwait(false) > 0;
        }
    }
}
