namespace Muzyk_API.Models
{
    public class Rating
    {
        public int UserId { get; set; }
        public string Genre { get; set; }
        public bool UserRating { get; set; }
    }
}