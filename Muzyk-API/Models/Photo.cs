using System;

namespace Muzyk_API.Models
{
    public class Photo : Media
    {
        public bool isProfilePhoto { get; set; }
        public bool isCoverPhoto { get; set; }
        public Photo()
        {
            MediaType = "Photo";
        }
    }
}