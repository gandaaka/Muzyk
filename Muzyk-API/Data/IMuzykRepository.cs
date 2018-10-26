using System.Collections.Generic;
using System.Threading.Tasks;
using Muzyk_API.Helpers;
using Muzyk_API.Models;

namespace Muzyk_API.Data
{
    public interface IMuzykRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Video> GetVideo(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Photo> GetCoverPhotoForUser(int userId);
        Task<Follow> GetFollow(int userId, int recepientId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessageForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recepientId);
        Task<Booking> GetBooking(int userId, int recepientId);
        Task<IEnumerable<int>> GetUserBookings(int id,bool bookers);
        IEnumerable<Booking> GetUserBooking(int userId);
    }
}