using System.ComponentModel.DataAnnotations;

namespace DotNetPractice.DTOS
{
    public class UserToRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength( 16, MinimumLength = 6, ErrorMessage = "Please specify a password of atleast 6 characters")]
        public string Password { get; set; }
    }
}