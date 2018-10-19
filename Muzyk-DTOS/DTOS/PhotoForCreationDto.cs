using System;
using Microsoft.AspNetCore.Http;

namespace Muzyk_DTOS
{
    public class PhotoForCreationDto
    {
        public string PhotoUrl { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}