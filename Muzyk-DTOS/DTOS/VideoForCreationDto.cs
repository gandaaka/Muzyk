using System;
using Microsoft.AspNetCore.Http;

namespace Muzyk_DTOS
{
    public class VideoForCreationDto
    {
        public int Id { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public string MediaType { get; set; }
        public VideoForCreationDto()
        {
            DateAdded = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        }
    }
}