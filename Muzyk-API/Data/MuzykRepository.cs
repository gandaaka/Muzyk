using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Muzyk_API.Helpers;
using Muzyk_API.Models;

namespace Muzyk_API.Data
{
    public class MuzykRepository : IMuzykRepository
    {
        private readonly DataContext _context;

        public MuzykRepository(DataContext context)
        {
            this._context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Follow> GetFollow(int userId, int recepientId)
        {
            return await _context.Follows.FirstOrDefaultAsync(u => u.FollowerId == userId && u.FolloweeId == recepientId);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.isProfilePhoto);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<Video> GetVideo(int id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);
            return video;
        }

        public async Task<Photo> GetCoverPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.isCoverPhoto);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).Include(v => v.Videos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos).Include(v => v.Videos).OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId);
            if (userParams.Genre != null || userParams.Genre != "")
            {
                switch (userParams.Genre)
                {
                    case "Rock":
                        users = users.Where(u => u.Genre == "Rock");
                        break;
                    case "Blues":
                        users = users.Where(u => u.Genre == "Blues");
                        break;
                    case "Jazz":
                        users = users.Where(u => u.Genre == "Jazz");
                        break;
                    case "Country":
                        users = users.Where(u => u.Genre == "Country");
                        break;
                    case "Dance":
                        users = users.Where(u => u.Genre == "Dance");
                        break;
                    case "Classical":
                        users = users.Where(u => u.Genre == "Classical");
                        break;
                    default:
                        userParams.Genre = "";
                        break;
                }
            }

            if (userParams.Followers)
            {
                var userFollowers = await GetUserFollowers(userParams.UserId, userParams.Followers);

                users = users.Where(u => userFollowers.Contains(u.Id));
            }

            if (userParams.Followees)
            {
                var userFollowees = await GetUserFollowers(userParams.UserId, userParams.Followers);

                users = users.Where(u => userFollowees.Contains(u.Id));
            }

            if (userParams.Bookers)
            {
                var userBookers = await GetUserBookings(userParams.UserId, userParams.Bookers);

                users = users.Where(u => userBookers.Contains(u.Id));
            }

            if (userParams.Bookees)
            {
                var userBookees = await GetUserBookings(userParams.UserId, userParams.Bookers);

                users = users.Where(u => userBookees.Contains(u.Id));
            }

            if (userParams.MinExp != 0 || userParams.MaxExp != 25)
            {
                var minExp = userParams.MinExp;
                var maxExp = userParams.MaxExp;
                users = users.Where(u => u.YearsOfExperience >= minExp && u.YearsOfExperience <= maxExp);
            }

            if (string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserFollowers(int id, bool followers)
        {
            var user = await _context.Users.Include(x => x.Follower)
            .Include(x => x.Followee).FirstOrDefaultAsync(u => u.Id == id);

            if (followers)
            {
                return user.Follower.Where(u => u.FolloweeId == id).Select(i => i.FollowerId);
            }
            else
            {
                return user.Followee.Where(u => u.FollowerId == id).Select(i => i.FolloweeId);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<PagedList<Message>> GetMessageForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recepient).ThenInclude(p => p.Photos)
                .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecepientId == messageParams.UserId && u.RecepientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SendersDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecepientId == messageParams.UserId && u.isRead == false && u.RecepientDeleted == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }
        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recepientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recepient).ThenInclude(p => p.Photos)
                .Where(m => m.RecepientId == userId && m.RecepientDeleted == false && m.SenderId == recepientId
                    || m.RecepientId == recepientId && m.SenderId == userId && m.SendersDeleted == false)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
        public async Task<Booking> GetBooking(int userId, int recepientId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(u => u.BookerId == userId && u.BookeeId == recepientId);
        }
        public async Task<IEnumerable<int>> GetUserBookings(int id, bool bookers)
        {
            var user = await _context.Users
                        .Include(x => x.Bookers)
                        .Include(x => x.Bookees)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (bookers)
            {
                return user.Bookers.Where(u => u.BookeeId == id).Select(i => i.BookerId);
            }
            else
            {
                return user.Bookees.Where(u => u.BookerId == id).Select(i => i.BookeeId);
            }


        }
        public IEnumerable<Booking> GetUserBooking(int userId)
        {
            var bookings = _context.Bookings.Where(u => u.BookerId == userId);
            return bookings;
        }
    }
}