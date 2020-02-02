using System.Collections.Generic;
using System.Threading.Tasks;
using Schedular.API.Models;
using Schedular.API.Helpers;

namespace Schedular.API.Data
{
    public interface ISchedular
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
    }
}
