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
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Follow>()
                .HasKey(k => new {k.FollowerId, k.FolloweeId});

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
        }
    }
}
