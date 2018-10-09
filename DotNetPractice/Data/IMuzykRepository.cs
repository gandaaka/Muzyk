using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPractice.Helpers;
using DotNetPractice.Models;

namespace DotNetPractice.Data
{
    public interface IMuzykRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
    }
}