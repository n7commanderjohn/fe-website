using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using FEWebsite.API.Core.Interfaces;
using FEWebsite.API.Controllers.DTOs.UserDTOs;
using FEWebsite.API.Helpers;
using FEWebsite.API.Core.Models;
using FEWebsite.API.Core.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Data.DerivedServices
{
    public class UserRepoService : BaseService, IUserRepoService
    {
        public UserRepoService(DataContext context) : base(context)
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

        public async Task<User> GetUser(int userId)
        {
            var user = await this.Context.Users
                .SingleOrDefaultAsync(u => u.Id == userId)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<User> GetUserThroughPasswordResetProcess(UserForPasswordResetDto userForPasswordResetDto)
        {
            var dto = userForPasswordResetDto;
            var user = await Context.Users
                .SingleOrDefaultAsync(u => u.Email == dto.Email && u.Username == dto.Username)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            IQueryable<User> users = await GetUsersFromContext(userParams).ConfigureAwait(false);

            var userList = await PagedList<User>
                .CreateAsync(users, userParams.PageNumber, userParams.PageSize)
                .ConfigureAwait(false);

            return userList;

            async Task<IQueryable<User>> GetUsersFromContext(UserParams userParams)
            {
                var users = this.Context.Users
                    .Where(u => u.Id != userParams.UserId);

                if (!string.IsNullOrEmpty(userParams.GenderId))
                {
                    users = users.Where(u => u.Gender.Id == userParams.GenderId);
                }

                if (userParams.Likers || userParams.Likees)
                {
                    var userLikerIds = await this.GetUserLikes(userParams).ConfigureAwait(false);
                    users = users.Where(u => userLikerIds.Contains(u.Id));
                }

                var orderBy = userParams.OrderBy?.ToLower() ?? nameof(User.LastLogin).ToLower();
                if (orderBy == nameof(User.AccountCreated).ToLower())
                {
                    users = users.OrderByDescending(u => u.AccountCreated);
                }
                else if (orderBy == nameof(User.LastLogin).ToLower())
                {
                    users = users.OrderByDescending(u => u.LastLogin);
                }
                else
                {
                    users = users.OrderByDescending(u => u.LastLogin);
                }

                bool ageParamsAreNonDefault = userParams.MinAge != 18 || userParams.MaxAge != 99;
                if (ageParamsAreNonDefault)
                {
                    // this subtracts ages by the current date to get the user age range.
                    var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                    var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                    users = users.Where(u => u.Birthday >= minDob && u.Birthday <= maxDob);
                }

                return users;
            }
        }

        private async Task<IEnumerable<int>> GetUserLikes(UserParams userParams)
        {
            var userId = userParams.UserId;
            var user = await this.Context.Users
                .FirstOrDefaultAsync(u => u.Id == userId)
                .ConfigureAwait(false);

            if (userParams.Likers)
            {
                return user.Likers
                    .Where(u => u.LikeeId == userId)
                    .Select(u => u.LikerId);
            }
            else
            {
                return user.Likees
                    .Where(u => u.LikerId == userId)
                    .Select(u => u.LikeeId);
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
            var photo = await this.Context.Photos
                .FirstOrDefaultAsync(p => p.Id == photoId)
                .ConfigureAwait(false);

            return photo;
        }

        private IQueryable<User> DefaultUserIncludes(bool expandedInclude = false)
        {
            if (expandedInclude)
            {
                return this.Context.Users
                    .Include(u => u.Photos)
                    .Include(u => u.Gender)
                    .Include(u => u.FavoriteGames)
                        .ThenInclude(ug => ug.Game)
                    .Include(u => u.FavoriteGenres)
                        .ThenInclude(ugg => ugg.GameGenre)
                    .Include(u => u.Likees);
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

        private IQueryable<User> UserIncludesLikes()
        {
            return this.Context.Users
                .Include(u => u.Likers)
                .Include(u => u.Likees);
        }

        public async Task<Photo> GetCurrentMainPhotoForUser(int userId)
        {
            return await this.Context.Photos
                .FirstOrDefaultAsync(p => p.UserId == userId && p.IsMain)
                .ConfigureAwait(false);
        }

        public async Task<Photo> SetUserPhotoAsMain(int userId, Photo photoToBeSet)
        {
            var currentMainPhoto = await this.GetCurrentMainPhotoForUser(userId).ConfigureAwait(false);
            if (currentMainPhoto != null)
            {
                currentMainPhoto.IsMain = false;
            }
            photoToBeSet.IsMain = true;

            return photoToBeSet;
        }

        public async Task<UserLike> GetLike(int userId, int recipientId)
        {
            return await this.Context.UserLikes
                .FirstOrDefaultAsync(u => u.LikerId == userId && u.LikeeId == recipientId)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<int>> GetLikes(int userId)
        {
            var likes = await GetLikesForSelectedUser(userId)
                .ToListAsync().ConfigureAwait(false);

            return likes;

            IQueryable<int> GetLikesForSelectedUser(int userId)
            {
                return this.Context.UserLikes
                    .Where(ul => ul.LikerId == userId)
                    .Select(ul => ul.LikeeId);
            }
        }

        public async Task<UserMessage> GetMessage(int id)
        {
            return await this.Context.UserMessages
                .FirstOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<PagedList<UserMessage>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = this.Context.UserMessages
                .AsQueryable();

            messages = messageParams.MessageContainer
            switch
            {
                MessageContainerArgs.Unread => messages.Where(um => um.RecipientId == messageParams.UserId && !um.IsRead && !um.RecipientDeleted),
                MessageContainerArgs.Inbox => messages.Where(um => um.RecipientId == messageParams.UserId && !um.RecipientDeleted),
                MessageContainerArgs.Outbox => messages.Where(um => um.SenderId == messageParams.UserId && !um.SenderDeleted),
                _ => messages.Where(um => um.RecipientId == messageParams.UserId && !um.IsRead && !um.RecipientDeleted),
            };

            messages = messages.OrderByDescending(um => um.MessageSent);

            var messageList = await PagedList<UserMessage>
                .CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize)
                .ConfigureAwait(false);

            return messageList;
        }

        private IIncludableQueryable<UserMessage, ICollection<Photo>> DefaultUserMessagesIncludes()
        {
            return this.Context.UserMessages
                .Include(um => um.Sender)
                    .ThenInclude(u => u.Photos)
                .Include(um => um.Recipient)
                    .ThenInclude(u => u.Photos);
        }

        public async Task<IEnumerable<UserMessage>> GetMessageThread(int userId, int recipientId)
        {
            var messageThread = await GetUserMessageThread(userId, recipientId)
                .OrderByDescending(um => um.MessageSent)
                .ToListAsync().ConfigureAwait(false);

            return messageThread;

            IQueryable<UserMessage> GetUserMessageThread(int userId, int recipientId)
            {
                return this.Context.UserMessages
                    .Where(um => (um.RecipientId == userId && !um.RecipientDeleted && um.SenderId == recipientId)
                        || (um.RecipientId == recipientId && !um.SenderDeleted && um.SenderId == userId));
            }
        }

        public bool DoesUserPhotoExist(User user, int photoId)
        {
            return user.Photos.Any(p => p.Id == photoId);
        }
    }
}
