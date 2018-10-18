using System;

namespace Muzyk_API.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
    }
}