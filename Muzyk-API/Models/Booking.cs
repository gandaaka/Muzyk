using System;

namespace Muzyk_API.Models
{
    public class Booking
    {
        public int BookerId { get; set; }
        public int BookeeId { get; set; }
        public User Booker { get; set; }
        public User Bookee { get; set; }
        public DateTime BookingDate { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}