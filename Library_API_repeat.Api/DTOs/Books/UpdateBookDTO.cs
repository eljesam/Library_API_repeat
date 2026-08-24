namespace Library_API_repeat.Api.DTOs.Books
{
    public class UpdateBookDTO
    {
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public int PublicationYear { get; set; }

        public bool IsAvailable { get; set; }

        public int AuthorId { get; set; }

       
    }
}
