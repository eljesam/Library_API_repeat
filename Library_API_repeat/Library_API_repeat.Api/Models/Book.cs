namespace Library_API_repeat.Api.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public int PublicationYear { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
