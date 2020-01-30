using System.Collections.Generic;
using System.Threading.Tasks;
using Schedular.API.Models;

namespace Schedular.API.Data
{
    public interface ISchedular
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}