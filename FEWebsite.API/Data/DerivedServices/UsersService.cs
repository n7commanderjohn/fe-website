using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using FEWebsite.API.DTOs.UserDTOs;
using FEWebsite.API.Helpers;

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
                .FirstOrDefaultAsync(UserIdMatches(userId))
                .ConfigureAwait(false);

            return user;

            static Expression<Func<User, bool>> UserIdMatches(int userId)
            {
                return u => u.Id == userId;
            }
        }

        public async Task<User> GetUserThroughPasswordResetProcess(UserForPasswordResetDto userForPasswordResetDto)
        {
            var user = await Context.Users
                .SingleOrDefaultAsync(UsernameAndEmailMatches(userForPasswordResetDto))
                .ConfigureAwait(false);

            return user;

            static Expression<Func<User, bool>> UsernameAndEmailMatches(UserForPasswordResetDto dto)
            {
                return u => u.Email == dto.Email && u.Username == dto.Username;
            }
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = this.DefaultUserIncludes()
                .Where(NotUserIdMatches(userParams));

            if (!string.IsNullOrEmpty(userParams.GenderId))
            {
                users = users.Where(GenderIdMatches(userParams));
            }

            bool ageParamsAreNonDefault = userParams.MinAge != 18 || userParams.MaxAge != 99;
            if (ageParamsAreNonDefault)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(UsersAreWithinAgeFilters(minDob, maxDob));
            }

            var userList = await PagedList<User>
                .CreateAsync(users, userParams.PageNumber, userParams.PageSize)
                .ConfigureAwait(false);

            return userList;

            static Expression<Func<User, bool>> NotUserIdMatches(UserParams userParams)
            {
                return u => u.Id != userParams.UserId;
            }

            static Expression<Func<User, bool>> GenderIdMatches(UserParams userParams)
            {
                return u => u.Gender.Id == userParams.GenderId;
            }

            static Expression<Func<User, bool>> UsersAreWithinAgeFilters(DateTime minDob, DateTime maxDob)
            {
                return u => u.Birthday >= minDob && u.Birthday <= maxDob;
            }
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
            var photo = await Context.Photos
                .FirstOrDefaultAsync(PhotoIdMatches(photoId))
                .ConfigureAwait(false);

            return photo;

            static Expression<Func<Photo, bool>> PhotoIdMatches(int photoId)
            {
                return p => p.Id == photoId;
            }
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
            return await Context.Photos
                .FirstOrDefaultAsync(UserIdMatchesAndPhotoIsMain(userId))
                .ConfigureAwait(false);

            static Expression<Func<Photo, bool>> UserIdMatchesAndPhotoIsMain(int userId)
            {
                return p => p.UserId == userId && p.IsMain;
            }
        }

        public async void SetUserPhotoAsMain(int userId, Photo photoToBeSet)
        {
            var currentMainPhoto = await this.GetCurrentMainPhotoForUser(userId).ConfigureAwait(false);
            currentMainPhoto.IsMain = false;
            photoToBeSet.IsMain = true;
        }
    }
}
