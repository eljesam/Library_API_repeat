using System.ComponentModel.DataAnnotations;
namespace Library_API_repeat.Api.DTOs.Authentication
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
