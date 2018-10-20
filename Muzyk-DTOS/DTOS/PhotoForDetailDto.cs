using System;

namespace Muzyk_DTOS
{
    public class PhotoForDetailDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isProfilePhoto { get; set; }
        public bool isCoverPhoto { get; set; }
    }
}