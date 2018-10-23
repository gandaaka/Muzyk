using System;

namespace Muzyk_API.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
    }
}