using Library_API_repeat.Api.Models;

namespace Library_API_repeat.Api.DTOs.Loans
{
    public class LoanDTO
    {
        public int id { get; set; }
        public int BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public int MemberId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
