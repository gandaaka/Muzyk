using System;

namespace Muzyk_DTOS.DTOS
{
    public class MediaForDetailDto
    {
        public int Id { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string MediaType { get; set; }     
    }
}