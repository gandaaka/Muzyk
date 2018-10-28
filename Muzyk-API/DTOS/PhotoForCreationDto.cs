using System;
using Microsoft.AspNetCore.Http;

namespace Muzyk_API.DTOS
{
    public class PhotoForCreationDto
    {
        public string MediaUrl { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public string MediaType { get; set; }
        public PhotoForCreationDto()
        {
            DateAdded = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
        }
    }
}