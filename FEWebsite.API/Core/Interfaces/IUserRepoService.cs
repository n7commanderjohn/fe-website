using System.Collections.Generic;
using System.Threading.Tasks;

using FEWebsite.API.Controllers.DTOs.UserDTOs;
using FEWebsite.API.Helpers;
using FEWebsite.API.Core.Models;
using FEWebsite.API.Core.Models.ManyToManyModels.ComboModels;

namespace FEWebsite.API.Core.Interfaces
{
    public interface IUserRepoService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<PagedList<User>> GetUsers(UserParams userParams);

        Task<User> GetUser(int userId);

        Task<User> GetUserThroughPasswordResetProcess(UserForPasswordResetDto userForPasswordResetDto);

        Task<IEnumerable<Gender>> GetGenders();

        Task<Photo> GetPhoto(int photoId);

        Task<Photo> GetCurrentMainPhotoForUser(int userId);

        Task<Photo> SetUserPhotoAsMain(int userId, Photo photoToBeSet);

        Task<UserLike> GetLike(int userId, int recipientId);

        Task<IEnumerable<int>> GetLikes(int userId);

        Task<UserMessage> GetMessage(int id);

        Task<PagedList<UserMessage>> GetMessagesForUser(MessageParams messageParams);

        Task<IEnumerable<UserMessage>> GetMessageThread(int userId, int recipientId);

        bool DoesUserPhotoExist(User user, int photoId);
    }
}
