namespace Library_API_repeat.Api.DTOs.Loans
{
    public class CreateLoanDTO
    {
        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
