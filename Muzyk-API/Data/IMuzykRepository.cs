using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPractice.Helpers;
using DotNetPractice.Models;

namespace DotNetPractice.Data
{
    public interface IMuzykRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Follow> GetFollow(int userId, int recepientId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessageForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recepientId);
    }
}