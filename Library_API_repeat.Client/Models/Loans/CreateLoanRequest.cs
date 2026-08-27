using System.ComponentModel.DataAnnotations;
namespace Library_API_repeat.Client.Models.Loans
{
    public class CreateLoanRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a book.")]
        public int BookId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a member.")]
        public int MemberId { get; set; }

        [Required]
        public DateTime LoanDate { get; set; } = DateTime.Today;

        [Required]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14);

        public DateTime? ReturnDate { get; set; }
    }
}
