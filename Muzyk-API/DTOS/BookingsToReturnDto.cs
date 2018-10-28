using System;

namespace Muzyk_API.DTOS
{
    public class BookingsToReturnDto
    {
        public int BookerId { get; set; }
        public int BookeeId { get; set; }
        public UserForListDto Booker { get; set; }
        public UserForListDto Bookee { get; set; } 
        public DateTime BookingDate { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}