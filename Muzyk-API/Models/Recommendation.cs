using System.Collections.Generic;

namespace Muzyk_API.Models
{
    public class Recommendation
    {
        public int RId { get; set; }
        public int UserId { get; set; }
        public int RelatedUser1 { get; set; }
        public int RelatedUser2 { get; set; }
        public int RelatedUser3 { get; set; }
        public int RelatedUser4 { get; set; }
        public int RelatedUser5 { get; set; }
        public ICollection<User> RecommendedUsers { get; set; }
    }
}