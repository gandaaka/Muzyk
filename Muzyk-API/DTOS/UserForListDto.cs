using System;

namespace Muzyk_API.DTOS
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int YearsOfExperience { get; set; }
        public string Introduction { get; set; }
        public string Genre { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}