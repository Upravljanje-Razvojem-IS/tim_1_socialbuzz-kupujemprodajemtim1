using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace AccountService.Dtos
{
    public class LoginDto
    {
        /// <summary>
        /// Users email.
        /// </summary>
        [Email]
        public string Email { get; set; }

        /// <summary>
        /// Users password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
