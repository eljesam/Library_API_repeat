namespace Library_API_repeat.Api.Models
{
    public class Loan
    {
        public int id { get; set; }
        public int BookId { get; set; }

        public Book? Book { get; set; }

        public string BorrowerName { get; set; } = string.Empty;

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
