using System;

namespace Muzyk_DTOS.DTOS
{
    public class BookingToCreateDto
    {
        public int RecipientId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
    }
}