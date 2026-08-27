using System.ComponentModel.DataAnnotations;

namespace Library_API_repeat.Client.Models.Loans
{
    public class UpdateLoanRequest
    {
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }

        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        [Required]
        public DateTime LoanDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
