using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Muzyk_API.DTOS
{
    public class VideoForCreationDto
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string MediaType { get; set; }
        public VideoForCreationDto()
        {
            DateAdded = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
        }
    }
}