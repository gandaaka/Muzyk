using System;
using Microsoft.AspNetCore.Http;

namespace Muzyk_DTOS
{
    public class VideoForCreationDto
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public VideoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}