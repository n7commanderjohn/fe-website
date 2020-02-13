using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using Microsoft.EntityFrameworkCore;
using FEWebsite.API.DTOs.UserDTOs;

namespace FEWebsite.API.Data.DerivedServices
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(DataContext context)
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
            var user = await this.DefaultUserIncludes(expandedInclude: true)
                .FirstOrDefaultAsync(u => u.Id == userId)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<User> GetUserThroughPasswordResetProcess(UserForPasswordResetDto userForPasswordResetDto)
        {
            var dto = userForPasswordResetDto;
            var user = await this.DefaultUserIncludes()
                .SingleOrDefaultAsync(u => u.Email == dto.Email && u.Username == dto.Username)
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

        public async Task<IEnumerable<Gender>> GetGenders()
        {
            var genders = await this.Context.Genders
                .ToListAsync()
                .ConfigureAwait(false);

            return genders;
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            var photo = await this.Context.Photos
                .FirstOrDefaultAsync(p => p.Id == photoId)
                .ConfigureAwait(false);

            return photo;
        }

        private IQueryable<User> DefaultUserIncludes(bool expandedInclude = false) {
            if (expandedInclude)
            {
                return this.Context.Users
                    .Include(u => u.Photos)
                    .Include(u => u.Gender)
                    .Include(u => u.FavoriteGames)
                        .ThenInclude(ug => ug.Game)
                    .Include(u => u.FavoriteGenres)
                        .ThenInclude(ugg => ugg.GameGenre);
            }
            else //for some reason when calling all users, it will never finish the api call
            {
                return this.Context.Users
                    .Include(u => u.Photos)
                    .Include(u => u.Gender)
                    .Include(u => u.FavoriteGames)
                    .Include(u => u.FavoriteGenres);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await this.Context
                .SaveChangesAsync()
                .ConfigureAwait(false) > 0;
        }

        public async Task<Photo> GetCurrentMainPhotoForUser(int userId)
        {
            return await this.Context.Photos
                .FirstOrDefaultAsync(p => p.UserId == userId && p.IsMain)
                .ConfigureAwait(false);
        }

        public async void SetUserPhotoAsMain(int userId, Photo photoToBeSet)
        {
            var currentMainPhoto = await this.GetCurrentMainPhotoForUser(userId).ConfigureAwait(false);
            currentMainPhoto.IsMain = false;
            photoToBeSet.IsMain = true;
        }
    }
}
