using System.ComponentModel.DataAnnotations;

namespace Muzyk_API.DTOS
{
    public class UserToPasswordChangeDto
    {   
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "Please specify a password of atleast {1} characters")]
        public string Password { get; set; }
    }
}