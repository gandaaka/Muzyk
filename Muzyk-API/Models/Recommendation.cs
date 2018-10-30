using System.Collections.Generic;

namespace Muzyk_API.Models
{
    public class Recommendation
    {
        public int RId { get; set; }
        public int UserId { get; set; }
        public int RecommendedUser1 { get; set; }
        public int RecommendedUser2 { get; set; }
        public int RecommendedUser3 { get; set; }
        public int RecommendedUser4 { get; set; }
        public int RecommendedUser5 { get; set; }
    }
}