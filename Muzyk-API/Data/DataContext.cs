using Microsoft.EntityFrameworkCore;
using Muzyk_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Muzyk_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Follow>()
                .HasKey(k => new { k.FollowerId, k.FolloweeId });

            builder.Entity<Follow>()
                .HasOne(u => u.Followee)
                .WithMany(u => u.Follower)
                .HasForeignKey(u => u.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follow>()
                .HasOne(u => u.Follower)
                .WithMany(u => u.Followee)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessageSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recepient)
                .WithMany(m => m.MessageRecieved)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ratings>()
                .HasKey(k => new { k.UserId, k.Genre });

            builder.Entity<Recommendation>()
                .HasKey(k => k.RId);

            builder.Entity<Booking>()
                .HasKey(k => new { k.BookerId, k.BookeeId, k.BookingDate });

            builder.Entity<Booking>()
                .HasOne(u => u.Bookee)
                .WithMany(u => u.Bookers)
                .HasForeignKey(u => u.BookeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(u => u.Booker)
                .WithMany(u => u.Bookees)
                .HasForeignKey(u => u.BookerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
