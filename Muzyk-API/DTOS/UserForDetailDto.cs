using System;
using System.Collections.Generic;

namespace Muzyk_API.DTOS
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int YearsOfExperience { get; set; }
        public string Genre { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public ICollection<PhotoForDetailDto> Photos { get; set; }
        public ICollection<VideoForDetailDto> Videos { get; set; }
    }
}