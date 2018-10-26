using System;
using System.Collections.Generic;
using Muzyk_API.Models;

namespace Muzyk_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string knownAs { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Genre { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int RId { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Follow> Follower { get; set; }
        public ICollection<Follow> Followee { get; set; }
        public ICollection<Message> MessageSent { get; set; }
        public ICollection<Message> MessageRecieved { get; set; }
        public ICollection<Booking> Bookers { get; set; }
        public ICollection<Booking> Bookees { get; set; }
    }
}