using System.Collections.Generic;

namespace Muzyk_API.Models
{
    public class Recommendation
    {
        public int RId { get; set; }
        public int UserId { get; set; }
        public ICollection<User> RecommendedUsers { get; set; }
    }
}