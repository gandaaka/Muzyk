using System;

namespace Muzyk_DTOS
{
    public class VideoForReturnDto
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}