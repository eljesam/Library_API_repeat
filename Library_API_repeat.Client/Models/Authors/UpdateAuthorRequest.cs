using System.ComponentModel.DataAnnotations;
namespace Library_API_repeat.Client.Models.Authors
{
    public class UpdateAuthorRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;
    }
}
