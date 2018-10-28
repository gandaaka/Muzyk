using System;

namespace Muzyk_API.DTOS
{
    public class VideoForReturnDto
    {
        public int Id { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}