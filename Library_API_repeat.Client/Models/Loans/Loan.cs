namespace Library_API_repeat.Client.Models.Loans
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public int MemberId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}
