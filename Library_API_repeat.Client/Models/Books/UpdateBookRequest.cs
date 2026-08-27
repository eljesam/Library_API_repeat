using System.ComponentModel.DataAnnotations;

namespace Library_API_repeat.Client.Models.Books
{
    public class UpdateBookRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; } = string.Empty;

        [Range(1, 3000)]
        public int PublicationYear { get; set; }

        public bool IsAvailable { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select an author.")]
        public int AuthorId { get; set; }
    }
}