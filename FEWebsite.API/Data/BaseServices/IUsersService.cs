using System.Collections.Generic;
using System.Threading.Tasks;
using FEWebsite.API.Models;

namespace FEWebsite.API.Data.BaseServices
{
    public interface IUsersService
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(int userId);

        Task<Photo> GetPhoto(int photoId);

        Task<Photo> GetCurrentMainPhotoForUser(int userId);

        void SetUserPhotoAsMain(int userId, Photo photoToBeSet);
    }
}