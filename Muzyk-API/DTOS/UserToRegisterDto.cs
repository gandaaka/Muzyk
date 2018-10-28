using System;
using System.ComponentModel.DataAnnotations;

namespace Muzyk_API.DTOS
{
    public class UserToRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Please specify a password of atleast {1} characters")]
        public string Password { get; set; }

        [Required]
        public string UserType { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public UserToRegisterDto()
        {
            Created = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            LastActive = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        }
    }
}